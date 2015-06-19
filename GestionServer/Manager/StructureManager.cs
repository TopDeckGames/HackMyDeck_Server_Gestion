using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Model;
using GestionServer.Data;

namespace GestionServer.Manager
{
    public class StructureManager
    {
        private List<Structure> structures;

        /// <summary>
        /// Récupère le détail des structures de l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Liste des structures</returns>
        public List<UserStructure> getUserStructures(int idUser)
        {
            try
            {
                return AdapterFactory.getStructureAdapter().getUserStructures(idUser);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Récupères les strucures du jeu
        /// </summary>
        /// <returns>Liste de structures</returns>
        public List<Structure> getStructures()
        {
            if (this.structures == null)
            {
                try
                {
                    this.structures = AdapterFactory.getStructureAdapter().getStructures();
                }
                catch
                {
                    throw;
                }
            }

            return this.structures;
        }
        
        /// <summary>
        /// Upgrade un bâtiment d'un niveau
        /// </summary>
        public void lvlUp(int idUser, int idStruct)
        {
            User user = new User();
            List<UserStructure> structures = new List<UserStructure>();

            try
            {
                user = AdapterFactory.getUserAdapter().getInfos(idUser);
                structures = AdapterFactory.getStructureAdapter().getUserStructures(idUser);

                UserStructure userStructure = structures.Where(i => i.id == idStruct).First();                

                int prix = 50 * userStructure.level;

                if (user.Credit >= prix && userStructure.level < 10)
                {
                    AdapterFactory.getStructureAdapter().lvlUp(idUser, idStruct);
                    AdapterFactory.getUserAdapter().setCredit(idUser, prix);
                }
            }
            catch
            {
                throw;
            }
        }
    }

}
