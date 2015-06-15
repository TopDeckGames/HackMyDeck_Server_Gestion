using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Data;

namespace GestionServer.Manager
{
    public class LeaderManager
    {
        /// <summary>
        /// Exécute les actions d'achat d'un leader
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idLeader">Identifiant du leader</param>
        public void buyLeader(int idUser, int idLeader)
        {
            if(!AdapterFactory.getLeaderAdapter().haveLeader(idUser, idLeader))
            {
                ManagerFactory.getDeckManager().addDeck(idUser, idLeader);
            }
            else
            {
                throw new Exception("Leader déjà possédé");
            }
        }
    }
}
