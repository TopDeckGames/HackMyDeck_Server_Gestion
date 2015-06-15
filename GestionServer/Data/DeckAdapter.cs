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

        public void getDecks(int idUser)
        {

        }
    }
}
