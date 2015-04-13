using System;
using System.IO;
using MySql.Data.MySqlClient;
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
        public List getOwnedCards(int idUser) {
          try
            {
                return AdapterFactory.getUserAdapter().getOwnedCards(idUser);
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
        public List getBuyableCards(int idUser) {
          try
            {
                return AdapterFactory.getUserAdapter().getBuyableCards(idUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ajoute une carte au deck de l'utilisateur
        /// </summary>
        /// <param name="idUser">idUser</param>
        /// <param name="idCard">idCard</param>
        public void addCard(int idUser, int idCard) {
          try
            {
                return AdapterFactory.getUserAdapter().addCard(idUser, idCard);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Récupère une carte
        /// </summary>
        /// <returns>Une carte</returns>
        /// <param name="idCard">idCard</param>
        public Card getCard(int idCard) {
          try
            {
                return AdapterFactory.getUserAdapter().getCards(idCard);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
  }
}
