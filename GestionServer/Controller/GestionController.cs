using System;
using System.Linq;
using System.Collections.Generic;
using GestionServer.Model;
using System.IO;
using GestionServer.Manager;
using GestionServer.Handlers;
using System.Configuration;
using GestionServer.Helper;

namespace GestionServer.Controller
{
    public class GestionController : IController
    {
        /// <summary>
        /// Redirige la requête vers l'action correspondante
        /// </summary>
        /// <param name="stream">Flux de données à traiter</param>
        public Response parser(Stream stream)
        {
            Response response = null;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                try
                {
                    ushort idAction = reader.ReadUInt16();

                    switch (idAction)
                    {
                        case 1:
                            response = this.enterCombatAction(
                                reader.ReadInt32(), 
                                reader.ReadInt32(),
                                reader.ReadUInt32()
                                );
                            break;
                        case 2:
                            response = this.leaveCombatAction(reader.ReadInt32());
                            break;
                        case 3:
                            response = this.getCards();
                            break;
                        case 4:
                            response = this.getStructures();
                            break;
                        default:
                            Logger.log(typeof(GestionController), "L'action n'existe pas : " + idAction, Logger.LogType.Error);
                            response = new Response();
                            response.addValue(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.log(typeof(GestionController), e.Message, Logger.LogType.Error);
                    response = new Response();
                    response.addValue(0);
                }
            }
            return response;
        }

        /// <summary>
        /// Appelle les actions nécessaires à l'enregistrement dans la file d'attente de combat
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idDeck">Identifiant du deck</param>
        /// <returns>Réponse</returns>
        private Response enterCombatAction(int idUser, int idDeck, uint token)
        {
            Object deck = null;
            User user = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                //deck = ManagerFactory.getDeckManager().getDeck(idUser, idDeck);
                ClientHandler handler = (ClientHandler)MainClass.Server.Handlers.Where(x => x.User.Id.Equals(idUser));
                handler.CombatToken = token;
                user = handler.User;
            }
            catch(Exception e)
            {
                Logger.log(typeof(GestionController), "Impossible de récupèrer le deck ou l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if(deck == null || user == null)
            {
                response.addValue(0);
            }
            else
            {
                user.CurrentServer = new Model.Server();
                user.CurrentServer.Name = ConfigurationManager.AppSettings["server_name"];
                ManagerFactory.getMasterManager().addInCombatQueue(user, deck);
                response.addValue(1);
            }

            return response;
        }

        /// <summary>
        /// Réalise les actions nécessaire afin de sortir l'utilisateur de la file d'attente de combat
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Réponse</returns>
        private Response leaveCombatAction(int idUser)
        {
            User user = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                ClientHandler handler = (ClientHandler)MainClass.Server.Handlers.Where(x => x.User.Id.Equals(idUser));
                user = handler.User;
            }
            catch (Exception e)
            {
                Logger.log(typeof(GestionController), "Impossible de récupèrer l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if (user == null)
            {
                response.addValue(0);
            }
            else
            {
                user.CurrentServer = new Model.Server();
                user.CurrentServer.Name = ConfigurationManager.AppSettings["server_name"];
                ManagerFactory.getMasterManager().leaveCombatQueue(user);
                response.addValue(1);
            }

            return response;
        }

        /// <summary>
        /// Récupère l'ensemble des cartes du jeu
        /// </summary>
        /// <returns>Response</returns>
        private Response getCards()
        {
            Response response = new Response();

            return response;
        }

        /// <summary>
        /// Récupère l'ensemble des structures du jeu
        /// </summary>
        /// <returns>Réponse</returns>
        private Response getStructures()
        {
            List<Structure> structures = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                structures = ManagerFactory.getStructureManager().getStructures();
            }
            catch(Exception e)
            {
                Logger.log(typeof(StructureManager), "Impossible de récupèrer l'ensemble des structures : " + e.Message, Logger.LogType.Error);
            }

            if(structures == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach(var structure in structures)
                {
                    response.addValue(structure.Id);
                    response.addValue(StringHelper.fillString(structure.Name, Structure.NAME_LENGTH));
                    response.addValue(StringHelper.fillString(structure.Description, Structure.DESCRIPTION_LENGTH));
                    response.addValue((int)structure.Type);
                }
            }

            return response;
        }
    }
}
