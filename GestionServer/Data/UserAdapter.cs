using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using GestionServer.Model;
using System.Data;

namespace GestionServer.Data
{
    public class UserAdapter : BaseAdapter
    {
        public UserAdapter()
            : base()
        {
        }  

        /// <summary>
        /// Renvoie les informations utilistateurs
        /// </summary>
        /// <param name="idUtilisateur">idUtilisateur.</param>
        public User getInfos(int idUtilisateur)
        {
            User user = null;
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT username, credit FROM user WHERE id = @idUtilisateur";
            cmd.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);

            try
            {
                base.connection.Open();              
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user = new User();
                        user.Login = (string)reader["username"];
                        user.Credit = (int)reader["credit"];  
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

            return user;  
        }
        
        
        /// <summary>
        /// Renvoie le resultats des 10 derniers matchs jouée par le joueur
        /// </summary>
        /// <param name="idUtilisateur">idUtilisateur.</param>
        public List<History> getHistory(int idUtilisateur)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT game.* FROM game, deck WHERE deck.user_id = @userId AND (game.firstToPlay_id = deck.user_id OR game.secondToPlay_id = deck.user_id) ORDER BY game.created DESC limit 10";
            cmd.Parameters.AddWithValue("@userId", idUtilisateur);
            List<History> historique = new List<History>();

            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            History histo = new History();
                            histo.Id = (int)reader["id"];
                            histo.FirstToPlay_id = (int)reader["firstToPlay_id"];
                            histo.Created = (DateTime)reader["created"];
                            histo.TotalDamage = (int)reader["totalDamage"];
                            histo.TotalUnit = (int)reader["totalUnit"];
                            histo.TotalTechno = (int)reader["totalTechno"];
                            histo.Winner = (int)reader["winner"];
                            histo.SecondToPlay_id = (int)reader["secondToPlay_id"];
                            historique.Add(histo);
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

            return historique;  
        }
    }
}
