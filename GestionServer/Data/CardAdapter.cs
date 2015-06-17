using GestionServer.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Data
{
    public class CardAdapter : BaseAdapter
    {
        /// <summary>
        /// Récupère l'ensemble des cartes du jeu
        /// </summary>
        /// <param name="idCard">idCard.</param>
        public List<Card> getCards()
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM card";
            List<Card> cartes = new List<Card>();
            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Card card = new Card();
                            card.Id = (int)reader["id"];
                            card.Title = (string)reader["title"];
                            card.Type = (Card.TypeCard)reader["type"];
                            card.Description = (string)reader["description"];
                            card.CostInGame = (int)reader["cost_in_game"];
                            card.CostInStore = (int)reader["cost_in_store"];
                            card.IsBuyable = (Boolean)reader["is_buyable"];
                            card.Rarity = (Card.TypeRarity)reader["rarity"];
                            cartes.Add(card);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }

            return cartes;
        }

        /// <summary>
        /// Récupère les cartes possèdées par un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Liste des cartes ainsi que leur quantité</returns>
        public Dictionary<Card, int> getOwnedCards(int idUser)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT c.id, u.quantity FROM card c INNER JOIN user_card u ON c.id = u.card_id WHERE u.user_id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);

            Dictionary<Card, int> cartes = new Dictionary<Card, int>();
            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Card card = new Card();
                            card.Id = (int)reader["id"];
                            cartes.Add(card, (int)reader["quantity"]);
                        }
                    }  
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }

            return cartes;
        }
    }
}
