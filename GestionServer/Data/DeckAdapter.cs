using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace GestionServer.Data
{
    public class DeckAdapter : BaseAdapter
    {
        public DeckAdapter()
            : base()
        {
        }
        
        /// <summary>
        /// Ajout un nouveau deck à l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="deck">Deck à enregistrer</param>
        /// <returns>Identifiant du deck</returns>
        public int addDeck(int idUser, Deck deck)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "INSERT INTO deck(user_id, leader_id) VALUES (@idUser, @idLeader)";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@idLeader", deck.Leader);

            try
            {
                base.connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }

            return 1;
        }

        public List<Deck> getDecks(int idUser)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT d.*, de.quantity FROM user u INNER JOIN deck d ON u.id = d.user_id INNER JOIN deck_card de ON d.id = de.deck_id WHERE u.id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            List<Deck> deck = null;

            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read()) 
                        {
                            Deck d = new Deck();
                            d.id = (int)reader["id"];
                            d.Leader = (int)reader["leader_id"];
                            d.name = (string)reader["name"];
                            d.color = (string)reader["color"];
                            d.Cards.Add((int)reader["card_id"], (int)reader["quantity"]);
                            deck.Add(d);
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

            return deck;  
        }
    }
}
