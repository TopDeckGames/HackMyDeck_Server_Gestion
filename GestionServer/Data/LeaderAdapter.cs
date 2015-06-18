using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using GestionServer.Model;

namespace GestionServer.Data
{
    public class LeaderAdapter : BaseAdapter
    {
        /// <summary>
        /// Teste si l'utilisateur possède un leader
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idLeader">Identifiant du leader</param>
        /// <returns></returns>
        public bool haveLeader(int idUser, int idLeader)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT count(*) as nb FROM deck WHERE user_id = @idUser AND leader_id = @idLeader";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@idLeader", idLeader);

            try
            {
                base.connection.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return (Int64)reader["nb"] > 0;
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

            return false;
        }

        /// <summary>
        /// Récupère l'ensemble des leaders du jeu
        /// </summary>
        /// <returns>Liste des leaders</returns>
        public List<Leader> getLeaders()
        {
            List<Leader> leaders = new List<Leader>();

            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT `id`, `name`, `description`, `effect`, `energy`, `price` FROM `leader`";
            
            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Leader leader = new Leader();
                            leader.Id = (int)reader["id"];
                            leader.Name = (string)reader["name"];
                            leader.Description = (string)reader["description"];
                            leader.Price = (int)reader["price"];
                            leader.Energy = (int)reader["energy"];
                            leader.Effect = (string)reader["effect"];

                            leaders.Add(leader);
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

            return leaders;
        }

        public List<int> getOwnedLeaders(int idUser)
        {
            List<int> leaders = new List<int>();

            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT l.leader_id FROM user u INNER JOIN user_leaders l ON u.id = l.user_id WHERE u.id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);

            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            leaders.Add((int)reader["leader_id"]);
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

            return leaders;
        }
    }
}
