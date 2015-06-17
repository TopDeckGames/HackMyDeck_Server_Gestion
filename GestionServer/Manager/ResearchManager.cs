using GestionServer.Data;
using GestionServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Manager
{
    public class ResearchManager
    {
        /// <summary>
        /// Récupère les arbres de talents
        /// </summary>
        public List<SkillTrees> getSkillTrees()
        {
            try
            {
                return AdapterFactory.getResearchAdapter().getSkillTrees();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Récupère l'arbre des talents d'un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        public List<UserSkillTrees> getSkillTrees(int idUser)
        {
            try
            {
               return AdapterFactory.getResearchAdapter().getUserSkillTrees(idUser);
            }
            catch
            {
                throw;
            }
        }
    }
}
