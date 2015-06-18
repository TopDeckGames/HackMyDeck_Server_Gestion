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

        /// <summary>
        /// Récupère les decks d'un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Liste de deck</returns>
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

        /// <summary>
        /// Sauvegarde l'état d'un deck
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="deck">Deck à enregistrer</param>
        public void saveDeck(int idUser, Deck deck)
        {
            try
            {
                Dictionary<Card, int> userCards = AdapterFactory.getCardAdapter().getOwnedCards(idUser);       

                //On vérifie que l'utilisateur possède les cartes du deck
                foreach (KeyValuePair<int, int> pair in deck.Cards)
                {
                    bool flag = false;
                    foreach(KeyValuePair<Card, int> userPair in userCards)
                    {
                        if(userPair.Key.Id.Equals(pair.Key) && userPair.Value.Equals(pair.Value))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if(!flag)
                    {
                        throw new Exception("L'utilisateur ne possède pas les cartes requises");
                    }
                }

                AdapterFactory.getDeckAdapter().saveDeck(deck);
            }
            catch
            {
                throw;
            }
        }
    }
}
