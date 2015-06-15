using System;

namespace GestionServer.Manager
{
    public static class ManagerFactory
    {
        private static MasterManager masterManager;
        private static CardManager cardManager;
        private static LeaderManager leaderManager;
        private static ResearchManager researchManager;
        private static StructureManager structureManager;
        private static DeckManager deckManager;

        /// <summary>
        /// Recupère le manager du serveur maitre
        /// </summary>
        /// <returns></returns>
        public static MasterManager getMasterManager()
        {
            if(ManagerFactory.masterManager == null)
            {
                ManagerFactory.masterManager = new MasterManager();
            }
            return ManagerFactory.masterManager;
        }

        /// <summary>
        /// Récupère le manager des cartes
        /// </summary>
        /// <returns></returns>
        public static CardManager getCardManager()
        {
            if (ManagerFactory.cardManager == null)
            {
                ManagerFactory.cardManager = new CardManager();
            }
            return ManagerFactory.cardManager;
        }

        /// <summary>
        /// Recupère le manager des Leaders
        /// </summary>
        /// <returns></returns>
        public static LeaderManager getLeaderManager()
        {
            if (ManagerFactory.leaderManager == null)
            {
                ManagerFactory.leaderManager = new LeaderManager();
            }
            return ManagerFactory.leaderManager;
        }

        /// <summary>
        /// Recupère le manager des structures
        /// </summary>
        /// <returns></returns>
        public static StructureManager getStructureManager()
        {
            if (ManagerFactory.structureManager == null)
            {
                ManagerFactory.structureManager = new StructureManager();
            }
            return ManagerFactory.structureManager;
        }

        /*

        /// <summary>
        /// Recupère le manager des recherches
        /// </summary>
        /// <returns></returns>
        public static ResearchManager getResearchManager()
        {
            if (ManagerFactory.researchManager == null)
            {
                ManagerFactory.researchManager = new ResearchManager();
            }
            return ManagerFactory.researchManager;
        }

         */

        /// <summary>
        /// Retourne le manager des decks
        /// </summary>
        /// <returns>Manager</returns>
        public static DeckManager getDeckManager()
        {
            if(ManagerFactory.deckManager == null)
            {
                ManagerFactory.deckManager = new DeckManager();
            }
            return ManagerFactory.deckManager;
        }
    }
}