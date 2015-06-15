using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Manager;
using GestionServer.Model;

namespace GestionServer.Controller
{
    public class UserController : IController
    {
        /// <summary>
        /// Redirige la requête vers l'action correspondante
        /// </summary>
        /// <param name="stream">Flux de données à traiter</param>
        public Response parser(Stream stream)
        {
            Response response;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                try
                {
                    ushort idAction = reader.ReadUInt16();

                    switch (idAction)
                    {
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

        private void getHistory(int idUser)
        {

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
                    response.addValue(structure.IdStructure);
                    response.addValue(structure.Level);
                    response.addValue(structure.Locked);
                    response.addValue(structure.Effectif);
                }
            }

            return response;
        }
    }
}
