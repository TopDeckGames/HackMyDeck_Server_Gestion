using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Model;
using GestionServer.Data;

namespace GestionServer.Manager
{
    public class CardManager
    {
        private List<Card> cards;

        /// <summary>
        /// Récupère les cartes possédées par l'utilisateur
        /// </summary>
        /// <returns>Une liste de carte possédées par le joueur</returns>
        /// <param name="idUser">idUser</param>
        public Dictionary<Card, int> getOwnedCards(int idUser)
        {
            try
            {
                return AdapterFactory.getCardAdapter().getOwnedCards(idUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Récupère les cartes qui peuvent être achetées par l'utilisateur
        /// </summary>
        /// <returns>Une liste de carte pouvant être achetées</returns>
        /// <param name="idUser">idUser</param>
        /* public List<Card> getBuyableCards(int idUser) {
          try
            {
                return AdapterFactory.getUserAdapter().getBuyableCards(idUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        } */

        /// <summary>
        /// Ajoute une carte au deck de l'utilisateur
        /// </summary>
        /// <param name="idUser">idUser</param>
        /// <param name="idCard">idCard</param>
        /*public void addCard(int idUser, int idCard) {
          try
            {
                return AdapterFactory.getUserAdapter().addCard(idUser, idCard);
            }
            catch (Exception e)
            {
                throw e;
            }
        } */


        /// <summary>
        /// Récupère toutes les cartes du jeu
        /// </summary>
        /// <returns>Liste de toute les cartes</returns>
        public List<Card> getCards()
        {
            if(this.cards == null)
            {
                try
                {
                    this.cards = AdapterFactory.getCardAdapter().getCards();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return this.cards;
        }
    }
}