using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

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
            cmd.CommandText = "SELECT count(*) as nb FROM deck WHERE idUser = @idUser AND idLeader = @idLeader";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@idLeader", idLeader);

            try
            {
                base.connection.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return (int)reader["nb"] > 0;
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
    }
}
