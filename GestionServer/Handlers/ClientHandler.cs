﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Collections.Generic;

using GestionServer.Controller;
using GestionServer.Helper;
using GestionServer.Model;

namespace GestionServer.Handlers
{
    public class ClientHandler
    {
        private TcpClient tcpClient;
        private int messageLength;
        private Thread requestActionThread;
        private volatile NetworkStream clientStream;
        private volatile byte[] requests;
        public uint CombatToken { get; set; }
        public User User { get; set; }
        private volatile bool active;
        public bool Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
            }
        }

        /// <summary>
        /// Initialise le ClientHandler
        /// </summary>
        /// <param name="client">Client TCP</param>
        public ClientHandler(object client)
        {
            this.tcpClient = (TcpClient)client;

            if (this.tcpClient.Connected)
            {
                Logger.log(typeof(ClientHandler), "Client connecté : " + ((IPEndPoint)this.tcpClient.Client.RemoteEndPoint).Address.ToString(), Logger.LogType.Info);
            }
            else
            {
                throw new Exception("Client non connecté");
            }

            //Récupèration de la taille des blocs
            this.messageLength = int.Parse(ConfigurationManager.AppSettings["message_length"]);
            this.Active = true;

            //On attend que le client envoi son id
            this.clientStream = this.tcpClient.GetStream();
            byte[] data = new byte[this.messageLength];
            int bytesRead = this.clientStream.Read(data, 0, this.messageLength);
            //On vérifie que l'id est autorisé
            using(MemoryStream ms = new MemoryStream(data))
            {
                using(BinaryReader reader = new BinaryReader(ms))
                {
                    int id = reader.ReadInt32();

                    foreach(KeyValuePair<User, DateTime> k in Server.AvailableUsers)
                    {
                        if(k.Key.Id.Equals(id))
                        {
                            DateTime time = DateTime.FromOADate(k.Value.ToOADate());
                            time = time.AddMinutes(10);                           
                            if(DateTime.Compare(time, DateTime.Now) >= 0)
                            {
                                this.User = k.Key;
                            }
                            break;
                        }
                    }
                }
            }

            if(this.User == null)
            {
                throw new Exception("Client non autorisé");
            }
            else
            {
                Server.AvailableUsers.Remove(this.User);
                Server.AvailableUsers.Add(this.User, DateTime.Now);
            }

            //Démarage du thread de traitement des requêtes
            this.requestActionThread = new Thread(new ThreadStart(requestAction));
            this.requestActionThread.Start();
        }

        /// <summary>
        /// Ecoute les requêtes du client
        /// </summary>
        public void handle()
        {
            if (this.tcpClient.Connected)
            {
                this.clientStream = this.tcpClient.GetStream();
                byte[] message = new byte[this.messageLength];
                int bytesRead;

                while (this.Active)
                {
                    bytesRead = 0;

                    try
                    {
                        bytesRead = clientStream.Read(message, 0, this.messageLength);
                    }
                    catch (Exception e)
                    {
                        Logger.log(typeof(ClientHandler), "Erreur lors de l'execution du socket : " + e.Message, Logger.LogType.Error);
                        break;
                    }

                    if (bytesRead == 0)
                    {
                        Logger.log(typeof(ClientHandler), "Connexion interrompue", Logger.LogType.Info);
                        break;
                    }

                    //Si la pile n'est pas nulle alors on empile
                    if (this.requests != null)
                    {
                        byte[] temp = new byte[this.requests.Length + message.Length];
                        this.requests.CopyTo(temp, 0);
                        message.CopyTo(temp, this.requests.Length);
                        this.requests = temp;
                    }
                    else //Sinon on initialise la pile
                    {
                        this.requests = new byte[message.Length];
                        message.CopyTo(this.requests, 0);
                    }

                    //On reset le chrono d'activité du joueur
                    Server.AvailableUsers.Remove(this.User);
                    Server.AvailableUsers.Add(this.User, DateTime.Now);
                }

                clientStream.Close();
                this.Active = false;
            }

            this.tcpClient.Close();
        }

        /// <summary>
        /// Traite les requêtes
        /// </summary>
        private void requestAction()
        {
            byte[] request;

            //Tant que le client est actif où qu'il reste des requêtes non traitées
            while (this.active || (this.requests != null && this.requests.Length > 0))
            {
                try
                {
                    request = this.getRequestPart();
                }
                catch
                {
                    continue;
                }

                Logger.log(typeof(ClientHandler), "Requete", Logger.LogType.Info);
                Stream stream = new MemoryStream(request);
                Response response = this.parser(stream);

                //Si le client est encore connecté on lui envoi la réponse
                this.sendResponse(response);
            }
        }

        /// <summary>
        /// Récupère plusieurs blocs de requête dans la pile
        /// </summary>
        /// <returns>Blocs de requête</returns>
        /// <param name="nb">Nombre de blocs à récupérer</param>
        private byte[] getRequestPart(int nb = 1)
        {
            byte[] request = new byte[this.messageLength * nb];

            //Si la pile contient assez de données
            if (this.requests != null)
            {
                if (this.requests.Length >= this.messageLength * nb)
                {
                    //On récupère les x premiers blocs
                    for (int i = 0; i < this.messageLength * nb; i++)
                    {
                        request[i] = (byte)this.requests.GetValue(i);
                    }

                    //On enlève les blocs récupérés de la pile
                    byte[] temp = new byte[this.requests.Length - this.messageLength * nb];
                    for (int i = this.messageLength * nb; i < this.requests.Length; i++)
                    {
                        temp[i - this.messageLength * nb] = (byte)this.requests.GetValue(i);
                    }
                    this.requests = temp;

                    return request;
                }
                else
                {
                    //Si le client est inactif et qu'il n'y a pas assez de données pour faire une requête alors on vide la pile
                    if (!this.Active && this.requests.Length >= this.messageLength)
                    {
                        this.requests = null;
                    }
                }
            }

            throw new Exception("Pas assez de données en mémoire");
        }

        /// <summary>
        /// Parse le flux en entrée et exécute la requête correspondante
        /// </summary>
        /// <param name="stream">Stream.</param>
        private Response parser(Stream stream)
        {
            Response response = null;
            uint token = uint.MinValue;
            ushort state = 0;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                try
                {
                    //Lecture de l'entête
                    token = reader.ReadUInt32();
                    ushort dataSize = reader.ReadUInt16();
                    ushort idController = reader.ReadUInt16();
                    char[] checksum = reader.ReadChars(32);

                    //Calcul du nombre de blocs à récupérer
                    int nbPart = dataSize / this.messageLength;
                    if (dataSize % this.messageLength > 0)
                    {
                        nbPart++;
                    }

                    //Récupèration des blocs
                    byte[] request;
                    try
                    {
                        request = this.getRequestPart(nbPart);
                    }
                    catch
                    {
                        Logger.log(typeof(ClientHandler), "La requête est parvenue incomplète", Logger.LogType.Error);
                        return null;
                    }

                    //Extraction des données des blocs
                    byte[] message = new byte[dataSize];
                    for (ushort i = 0; i < dataSize; i++)
                    {
                        message[i] = (byte)request.GetValue(i);
                    }

                    //Vérification de l'intégrité des données
                    if (Checksum.verify(message, new string(checksum)))
                    {
                        Stream dataStream = new MemoryStream(message);

                        switch (idController)
                        {
                            case 1:
                                response = ControllerFactory.getUserController().parser(this.User, dataStream);
                                state = 1;
                                break;
                            case 2:
                                response = ControllerFactory.getGestionController().parser(this.User, dataStream);
                                state = 1;
                                break;
                            case 3:
                                response = ControllerFactory.getDeckController().parser(this.User, dataStream);
                                state = 1;
                                break;
                            default:
                                Logger.log(typeof(ClientHandler), "Le controlleur n'existe pas " + idController, Logger.LogType.Error);
                                break;
                        }
                    }
                    else
                    {
                        Logger.log(typeof(ClientHandler), "Les données sont érronées", Logger.LogType.Error);
                    }
                }
                catch (Exception e)
                {
                    Logger.log(typeof(ClientHandler), e.Message, Logger.LogType.Error);
                }
            }

            //Écriture de l'entête
            try
            {
                byte[] responseContent = new byte[0];
                if (response != null)
                    responseContent = response.getResponse();

                Response finalResponse = new Response();
                finalResponse.openWriter();

                finalResponse.addValue(token);
                finalResponse.addValue(ushort.Parse(responseContent.Length.ToString()));
                finalResponse.addValue(state);
                finalResponse.addValue(Checksum.create(responseContent));
                finalResponse.addValue(responseContent);

                response = finalResponse;
            }
            catch (Exception e)
            {
                Logger.log(typeof(ClientHandler), "Impossible d'écrire la réponse : " + e.Message, Logger.LogType.Fatal);
                response = new Response();
            }

            return response;
        }

        /// <summary>
        /// Détermine si le client à terminé son exécution
        /// </summary>
        /// <returns></returns>
        public bool isActive()
        {
            return this.Active || this.requests != null;
        }

        /// <summary>
        /// Envoi une réponse au client
        /// </summary>
        /// <param name="response">Réponse à envoyer</param>
        public void sendResponse(Response response)
        {
            if (this.tcpClient.Connected)
            {
                byte[] data = response.getResponse();
                response.closeWriter();

                //Complète de façons à ce que la réponse soit un multiple de la longueur du message définie
                int diff = this.messageLength - data.Length % this.messageLength;
                if (diff > 0)
                {
                    byte[] temp = new byte[data.Length + diff];
                    data.CopyTo(temp, 0);
                    for (int i = 0; i < diff; i++)
                    {
                        temp[data.Length + i] = 0;
                    }
                    data = temp;
                }

                this.tcpClient.Client.Send(data, data.Length, SocketFlags.None);
            }
        }

        /// <summary>
        /// Envoi au client l'annonce de début de combat
        /// </summary>
        /// <param name="server">Serveur vers lequel rediriger le joueur</param>
        public void enterCombat(Model.Server server)
        {
            Response resp = new Response();
            resp.addValue((ushort)1);
            //Ajout des données de connexion au server
            resp.addValue(BitConverter.ToInt32(server.Address.GetAddressBytes(), 0));
            resp.addValue(server.Port);

            //Écriture de l'entête
            try
            {
                byte[] responseContent = new byte[0];
                if (resp != null)
                    responseContent = resp.getResponse();

                Response finalResponse = new Response();
                finalResponse.openWriter();
                finalResponse.addValue(this.CombatToken);
                finalResponse.addValue(ushort.Parse(responseContent.Length.ToString()));
                finalResponse.addValue((ushort)0);
                finalResponse.addValue(Checksum.create(responseContent));
                finalResponse.addValue(responseContent);

                resp = finalResponse;
            }
            catch (Exception e)
            {
                Logger.log(typeof(MonitorHandler), "Impossible d'écrire la réponse : " + e.Message, Logger.LogType.Fatal);
                resp = new Response();
            }

            this.sendResponse(resp);
        }
    }
}