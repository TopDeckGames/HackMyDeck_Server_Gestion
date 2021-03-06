﻿using System;
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

                cmd.CommandText = "SELECT id from deck WHERE user_id = @idUser AND leader_id = @idLeader";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return (int)reader["id"];
                    }
                    else
                    {
                        throw new Exception("Impossible de récupèrer l'identifiant du deck");
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
        }

        /// <summary>
        /// Récupère la liste des decks d'un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Liste de decks</returns>
        public List<Deck> getDecks(int idUser)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT d.*, de.quantity FROM user u INNER JOIN deck d ON u.id = d.user_id INNER JOIN deck_card de ON d.id = de.deck_id WHERE u.id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            List<Deck> deck = new List<Deck>();

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
                            d.Id = (int)reader["id"];
                            d.Leader = (int)reader["leader_id"];
                            d.Name = (string)reader["name"];
                            d.Color = (string)reader["color"];
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

        /// <summary>
        /// Met à jour les valeurs d'un deck
        /// </summary>
        /// <param name="deck">Deck à enregistrer</param>
        public void saveDeck(Deck deck)
        {
            try
            {
                base.connection.Open();

                //Suppression des cartes liées au deck
                MySqlCommand cmd = base.connection.CreateCommand();
                cmd.CommandText = "DELETE FROM deck_card WHERE deck_id = @deckId";
                cmd.Parameters.AddWithValue("@deckId", deck.Id);
                cmd.ExecuteNonQuery();

                //Enregistrement des cartes
                StringBuilder str = new StringBuilder();
                foreach (KeyValuePair<int, int> pair in deck.Cards)
                {
                    str.AppendLine("INSERT INTO`deck_card(deck_id, card_id, quantity) VALUES (@deckId, @cardId" + pair.Key + ", @quantity" + pair.Key + ");");
                    cmd.Parameters.AddWithValue("@cardId" + pair.Key, pair.Key);
                    cmd.Parameters.AddWithValue("@quantity" + pair.Key, pair.Value);
                }
                cmd.CommandText = str.ToString();
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
        }
    }
}
