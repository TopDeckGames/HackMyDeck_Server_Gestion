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
        private static List<SkillTrees> skillTrees;

        /// <summary>
        /// Récupère les arbres de talents
        /// </summary>
        public List<SkillTrees> getSkillTrees()
        {
            if(ResearchManager.skillTrees == null)
            {
                try
                {
                    ResearchManager.skillTrees = AdapterFactory.getResearchAdapter().getSkillTrees();
                }
                catch
                {
                    throw;
                }
            }
            return ResearchManager.skillTrees;
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
