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
        /// <summary>
        /// Récupère les cartes possédées par l'utilisateur
        /// </summary>
        /// <returns>Une liste de carte possédées par le joueur</returns>
        /// <param name="idUser">idUser</param>
       public List<Card> getOwnedCards(int idUser) {
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
        /// Récupère une carte
        /// </summary>
        /// <returns>Liste de toute les cartes</returns>
        public List<Card> getCards() {
          try
            {
                return AdapterFactory.getCardAdapter().getCards();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
  }
}

