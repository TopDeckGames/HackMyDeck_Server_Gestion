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
        /// Renvoie la carte
        /// </summary>
        /// <param name="idCard">idCard.</param>
        public List<Card> getCards()
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM card";
            List<Card> cartes = null;
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
                            card.Titre = (string)reader["title"];
                            card.Type = (GestionServer.Model.Card.TypeCard)reader["type"];
                            card.Description = (string)reader["description"];
                            card.CostInGame = (int)reader["cost_in_game"];
                            card.CostInStore = (int)reader["cost_in_store"];
                            card.IsBuyable = (Boolean)reader["is_buyable"];
                            card.Rarity = (GestionServer.Model.Card.TypeRarity)reader["rarity"];
                            cartes.Add(card);
                        }
                    }
                return cartes;
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
        }

        public List<Card> getOwnedCards(int idUser)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT c.id, c.title, c.type, c.description, c.cost_in_game, c.cost_in_store, c.is_buyable, c.rarity FROM card c INNER JOIN user_card u ON c.id = u.card_id WHERE u.user_id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);

            List<Card> cartes = null;
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
                            card.Titre = (string)reader["title"];
                            card.Type = (GestionServer.Model.Card.TypeCard)reader["type"];
                            card.Description = (string)reader["description"];
                            card.CostInGame = (int)reader["cost_in_game"];
                            card.CostInStore = (int)reader["cost_in_store"];
                            card.IsBuyable = (Boolean)reader["is_buyable"];
                            card.Rarity = (GestionServer.Model.Card.TypeRarity)reader["rarity"];
                            cartes.Add(card);
                        }
                    }
                    return cartes;
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
        }
    }
}
