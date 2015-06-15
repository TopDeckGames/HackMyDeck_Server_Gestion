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
            try
            {
                return AdapterFactory.getStructureAdapter().getStructures();
            }
            catch
            {
                throw;
            }
        }
    }
}
