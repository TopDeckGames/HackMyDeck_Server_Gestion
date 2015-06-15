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
        public User getInfos(string idUtilisateur)
        {
            return null;
        }
    }
}
