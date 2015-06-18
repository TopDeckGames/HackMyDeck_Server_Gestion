using GestionServer.Data;
using GestionServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Manager
{
    public class UserManager
    {

        /// <summary>
        /// Récupère un utilisateur à partir de ses identifiants
        /// </summary>
        /// <returns>Un utilisteur</returns>
        /// <param name="idUtilisateur">IdUtilisateur</param>
        public User getUser(int idUtilisateur)
        {
            try
            {
                return AdapterFactory.getUserAdapter().getInfos(idUtilisateur);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<History> getHistory(int idUtilisateur)
        {
            try
            {
                return AdapterFactory.getUserAdapter().getHistory(idUtilisateur);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void setCredit(int idUser, int prix)
        {
            try
            {
                AdapterFactory.getUserAdapter().setCredit(idUser, prix);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
