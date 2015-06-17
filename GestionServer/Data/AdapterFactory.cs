﻿using System;

namespace GestionServer.Data
{
    public static class AdapterFactory
    {
        private static UserAdapter userAdapter;
        private static DeckAdapter deckAdapter;
        private static LeaderAdapter leaderAdapter;
        private static StructureAdapter structureAdapter;
        private static CardAdapter cardAdapter;
        private static ResearchAdapter researchAdapter;

        /// <summary>
        /// Retourne l'instance de l'adapter des utilisateurs
        /// </summary>
        /// <returns>L'adapteur des utilisateurs</returns>
        public static UserAdapter getUserAdapter()
        {
            if (AdapterFactory.userAdapter == null)
            {
                AdapterFactory.userAdapter = new UserAdapter();
            }
            return AdapterFactory.userAdapter;
        }

        /// <summary>
        /// Retourne l'instance de l'adapter des cartes
        /// </summary>
        /// <returns>L'adapteur des cartes</returns>
        public static CardAdapter getCardAdapter()
        {
            if (AdapterFactory.cardAdapter == null)
            {
                AdapterFactory.cardAdapter = new CardAdapter();
            }
            return AdapterFactory.cardAdapter;
        }

        /// <summary>
        /// Retourne l'instance de l'adapter des decks
        /// </summary>
        /// <returns>Adapter</returns>
        public static DeckAdapter getDeckAdapter()
        {
            if(AdapterFactory.deckAdapter == null)
            {
                AdapterFactory.deckAdapter = new DeckAdapter();
            }
            return AdapterFactory.deckAdapter;
        }

        /// <summary>
        /// Retourne l'instance de l'adapter des leaders
        /// </summary>
        /// <returns>Adapter</returns>
        public static LeaderAdapter getLeaderAdapter()
        {
            if(AdapterFactory.leaderAdapter == null)
            {
                AdapterFactory.leaderAdapter = new LeaderAdapter();
            }
            return AdapterFactory.leaderAdapter;
        }

        /// <summary>
        /// Retourne l'instance de l'adapter des structures
        /// </summary>
        /// <returns>Adapter</returns>
        public static StructureAdapter getStructureAdapter()
        {
            if(AdapterFactory.structureAdapter == null)
            {
                AdapterFactory.structureAdapter = new StructureAdapter();
            }
            return AdapterFactory.structureAdapter;
        }

        public static ResearchAdapter getResearchAdapter()
        {

            if (AdapterFactory.researchAdapter == null)
            {
                AdapterFactory.researchAdapter = new ResearchAdapter();
            }
            return AdapterFactory.researchAdapter;
        }
    }
}