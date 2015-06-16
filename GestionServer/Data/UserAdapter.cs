﻿using System;
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
        public User getInfos(string idUtilisateur)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT username, credit FROM user WHERE id = @idUtilisateur";
            cmd.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);

            try
            {
                base.connection.Open();
                User user = null;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user = new User();
                        user.Login = (string)reader["username"];
                        user.Credit = (int)reader["credit"];  
                    }
                    return user;  
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
        /// Renvoie le resultats des 10 derniers matchs jouée par le joueur
        /// </summary>
        /// <param name="idUtilisateur">idUtilisateur.</param>
        public List<Object> getHistory(string idUtilisateur)
        {
            return null;
        }
    }
}
