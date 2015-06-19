using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Manager;
using GestionServer.Model;
using GestionServer.Helper;

namespace GestionServer.Controller
{
    public class UserController : IController
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
                            response = this.getStructures(user.Id);
                            break;
                        case 2:
                            response = this.buyLeader(user.Id, reader.ReadInt32());
                            break;
                        case 3:
                            response = this.buyCard(user.Id, reader.ReadInt32());
                            break;
                        case 4:
                            response = this.getInfos(user.Id);
                            break;
                        case 5:
                            response = this.getHistory(user.Id);
                            break;
                        case 6:
                            response = this.getLeaders(user.Id);
                            break;
                        case 7:
                            response = this.getSkillTrees(user.Id);
                            break;
                        case 8:
                            response = this.levelUp(user.Id, reader.ReadInt32());
                            break;
                        default:
                            Logger.log(typeof(UserController), "L'action n'existe pas : " + idAction, Logger.LogType.Error);
                            response = new Response();
                            response.addValue(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.log(typeof(UserController), e.Message, Logger.LogType.Error);
                    response = new Response();
                    response.addValue(0);
                }
            }
            return response;
        }

        /// <summary>
        /// Récupères les informations des structures de l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Réponse</returns>
        private Response getStructures(int idUser)
        {
            List<UserStructure> structures = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                structures = ManagerFactory.getStructureManager().getUserStructures(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(StructureManager), "Impossible de récupèrer les structures de l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if(structures == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                //Ajout des valeurs des structures
                foreach(var structure in structures)
                {
                    response.addValue(structure.id);
                    response.addValue(structure.level);
                    response.addValue(structure.Locked);
                    response.addValue(structure.Effectif);
                }
            }

            return response;
        }

        /// <summary>
        /// Achat d'un leader
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idLeader">Identifiant du leader</param>
        /// <returns>Réponse</returns>
        private Response buyLeader(int idUser, int idLeader)
        {
            Response response = new Response();
            response.openWriter();

            try
            {
                int idDeck = ManagerFactory.getLeaderManager().buyLeader(idUser, idLeader);
                response.addValue(1);
                response.addValue(idLeader);
                response.addValue(idDeck);
            }
            catch(Exception e)
            {
                Logger.log(typeof(LeaderManager), "Impossible d'acheter le leader : " + e.Message, Logger.LogType.Error);
                response.addValue(0);
            }

            return response;
        }

        /// <summary>
        /// Achat d'une carte
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idCard">Identifiant de la carte</param>
        /// <returns>Réponse</returns>
        private Response buyCard(int idUser, int idCard)
        {
            Response response = new Response();

            return response;
        }

        /// <summary>
        /// Récupère les informations de l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Réponse</returns>
        private Response getInfos(int idUser)
        {
            User user = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                user = ManagerFactory.getUserManager().getUser(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(UserManager), "Impossible de récupèrer les informations de l'utilisateur : " + e.Message, Logger.LogType.Error);
            }
            
            if(user == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                response.addValue(StringHelper.fillString(user.Login, User.LOGIN_LENGTH));
                response.addValue(user.Credit);
            }

            return response;
        }

        /// <summary>
        /// Récupère les leaders possédés par un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Response</returns>
        private Response getLeaders(int idUser)
        {
            List<int> leaders = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                leaders = ManagerFactory.getLeaderManager().getOwnedLeaders(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(LeaderManager), "Impossible de récupèrer les leaders de l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if(leaders == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach(int leader in leaders)
                {
                    response.addValue(leader);
                }
            }

            return response;
        }

        /// <summary>
        /// Récupère l'historique des parties d'un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Response</returns>
        private Response getHistory(int idUser)
        {
            List<History> history = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                history = ManagerFactory.getUserManager().getHistory(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(UserManager), "Impossible de récupèrer l'historique de l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if(history == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach (var item in history)
                {
                    int id = item.FirstToPlay_id == idUser ? item.SecondToPlay_id : item.FirstToPlay_id;
                    User ennemy = null;

                    try
                    {
                        ennemy = ManagerFactory.getUserManager().getUser(id);
                    }
                    catch(Exception e)
                    {
                        Logger.log(typeof(UserManager), "Impossible de récupèrer les informations de l'utilisateur : " + e.Message, Logger.LogType.Error);
                    }

                    String ennemyName;
                    if (ennemy == null)
                    {
                        ennemyName = "Inconnu";
                    }
                    else
                    {
                        ennemyName = ennemy.Login;
                    }

                    response.addValue(item.Id);
                    response.addValue(StringHelper.fillString(ennemyName, User.LOGIN_LENGTH));
                    response.addValue((Int32)item.Created.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                    response.addValue(item.TotalDamage);
                    response.addValue(item.TotalTechno);
                    response.addValue(item.TotalUnit);
                    response.addValue(item.Winner.Equals(idUser));
                }
            }

            return response;
        }

        /// <summary>
        /// Récupère l'ensemble des informations des skillTrees d'un l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Response</returns>
        private Response getSkillTrees(int idUser)
        {
            List<UserSkillTrees> skillTrees = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                skillTrees = ManagerFactory.getResearchManager().getSkillTrees(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(ResearchManager), "Impossible de récupèrer les SkillTrees de l'utilisateur : " + e.Message, Logger.LogType.Error);
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
                    response.addValue(item.Skill_id);
                    response.addValue(item.LastEnhancement_id);
                }
            }

            return response;
        }

        /// <summary>
        /// Monte le level d'un batiment
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idStructure">Identifiant de la structure</param>
        /// <returns>Response</returns>
        private Response levelUp(int idUser, int idStructure)
        {
            Response response = new Response();
            response.openWriter();

            try
            {
                ManagerFactory.getStructureManager().lvlUp(idUser, idStructure);
                response.addValue(1);
            }
            catch(Exception e)
            {
                Logger.log(typeof(StructureManager), "Impossible de changer le niveau de la structure : " + e.Message, Logger.LogType.Error);
                response.addValue(0);
            }

            return response;
        }
    }
}
