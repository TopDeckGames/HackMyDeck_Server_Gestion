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
        /// Renvoie la carte
        /// </summary>
        /// <param name="idCard">idCard.</param>
        public Card getCard(int idCard)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = ""; // Request SQL Here pour récuperer carte (CHECK BDD)!
            cmd.Parameters.AddWithValue("@idCard", idCard);
            MySqlDataReader reader;

            try
            {
                base.connection.Open();
                reader = cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }

            // On check si la carte existe bien en base !
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();

                if (dt.Rows.Count == 1) // Si on a trouvé la carte, on la renvoie avec tte les infos.
                {
                    DataRow row = dt.Rows[0];
                    //Card card = new Card(); Check BDD
                    //return card;
                }
            }
            reader.Close();
            return null;
        }
    }
}
