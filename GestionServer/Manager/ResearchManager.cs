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
        private static List<Enhancement> enhancements;

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

        /// <summary>
        /// Déclenche une recherche
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idEnhancement">Identifiant de la recherche</param>
        public void setCurrentResearch(int idUser, int idEnhancement)
        {
            try
            {
                if (ResearchManager.enhancements == null)
                {
                    this.getEnhancements();
                }

                Enhancement enhancement =   (Enhancement)from item in ResearchManager.enhancements
                                            where item.Id == idEnhancement
                                            select item;

                if(enhancement.Parent == 0 || AdapterFactory.getResearchAdapter().isParentEnhancementUnlocked(idUser, idEnhancement))
                {

                }
                else
                {
                    throw new Exception("Impossible d'effectuer cette recherche, les prérequis ne sont pas validés");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retourne la liste de tous les enhancements du jeu
        /// </summary>
        /// <returns></returns>
        public List<Enhancement> getEnhancements()
        {
            if (ResearchManager.enhancements == null)
            {
                try
                {

                }
                catch
                {
                    throw;
                }
            }
            return ResearchManager.enhancements;
        }
    }
}
