using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Model;
using GestionServer.Data;

namespace GestionServer.Manager
{
    public class DeckManager
    {
        /// <summary>
        /// Ajoute un nouveau deck
        /// </summary>
        /// <param name="name">Libelle du deck</param>
        /// <param name="idLeader">Leader lié au deck</param>
        /// <returns>Id du deck</returns>
        public int addDeck(int idUser, int idLeader)
        {
            Deck deck = new Deck();
            deck.Leader = idLeader;

            try
            {
                return AdapterFactory.getDeckAdapter().addDeck(idUser, deck);
            }
            catch
            {
                return 0;
            }
        }

        public List<Deck> getDecks(int idUser)
        {
            try
            {
                return AdapterFactory.getDeckAdapter().getDecks(idUser);
            }
            catch
            {
                return null;
            }
        }
    }
}
