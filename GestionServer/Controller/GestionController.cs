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
        public Response parser(User user, Stream stream)
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
                                user.Id, 
                                reader.ReadInt32(),
                                reader.ReadUInt32()
                                );
                            break;
                        case 2:
                            response = this.leaveCombatAction(user.Id);
                            break;
                        case 3:
                            response = this.getCards();
                            break;
                        case 4:
                            response = this.getStructures();
                            break;
                        case 5:
                            response = this.getLeaders();
                            break;
                        case 6:
                            response = this.getSkillTrees();
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
            List<Card> cards = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                cards = ManagerFactory.getCardManager().getCards();
            }
            catch(Exception e)
            {
                Logger.log(typeof(CardManager), "Impossible de récupèrer l'ensemble des cartes : " + e.Message, Logger.LogType.Error);
            }

            if(cards == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach (var card in cards)
                {
                    response.addValue(card.Id);
                    response.addValue(StringHelper.fillString(card.Title, Card.TITLE_LENGTH));
                    response.addValue(StringHelper.fillString(card.Description, Card.DESCRIPTION_LENGTH));
                    response.addValue((int)card.Rarity);
                    response.addValue(card.CostInGame);
                    response.addValue(card.CostInStore);
                    response.addValue(card.IsBuyable);
                }
            }

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
                    response.addValue(structure.PosX);
                    response.addValue(structure.PosY);
                    response.addValue(structure.Width);
                    response.addValue(structure.Height);
                }
            }

            return response;
        }

        /// <summary>
        /// Récupère l'ensemble des leaders du jeu
        /// </summary>
        /// <returns>Response</returns>
        private Response getLeaders()
        {
            List<Leader> leaders = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                leaders = ManagerFactory.getLeaderManager().getLeaders();
            }
            catch(Exception e)
            {
                Logger.log(typeof(LeaderManager), "Impossible de récupèrer l'ensemble des leaders : " + e.Message, Logger.LogType.Error);
            }

            if(leaders == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach(var leader in leaders)
                {
                    response.addValue(leader.Id);
                    response.addValue(StringHelper.fillString(leader.Name, Leader.NAME_LENGTH));
                    response.addValue(StringHelper.fillString(leader.Description, Leader.DESCRIPTION_LENGTH));
                    response.addValue(leader.Price);
                    response.addValue(leader.Energy);
                    response.addValue(StringHelper.fillString(leader.Effect, Leader.EFFECT_LENGTH));
                }
            }

            return response;
        }

        /// <summary>
        /// Récupère l'ensemble des skilltrees du jeu
        /// </summary>
        /// <returns>Response</returns>
        private Response getSkillTrees()
        {
            List<SkillTrees> skillTrees = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                skillTrees = ManagerFactory.getResearchManager().getSkillTrees();
            }
            catch(Exception e)
            {
                Logger.log(typeof(ResearchManager), "Impossible de récupèrer l'ensemble des SkillTrees : " + e.Message, Logger.LogType.Error);
            }

            if(skillTrees == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach(var item in skillTrees)
                {
                    response.addValue(item.Id);
                    response.addValue(StringHelper.fillString(item.Label, SkillTrees.LABEL_LENGTH));
                    response.addValue(item.Type);
                }
            }

            return response;
        }
    }
}
