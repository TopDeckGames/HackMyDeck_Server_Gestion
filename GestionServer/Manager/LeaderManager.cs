﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Data;
using GestionServer.Model;

namespace GestionServer.Manager
{
    public class LeaderManager
    {
        private List<Leader> leaders;

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

        /// <summary>
        /// Retourne la liste de tous les leaders du jeu
        /// </summary>
        /// <returns>Liste de leaders</returns>
        public List<Leader> getLeaders()
        {
            if(this.leaders == null)
            {
                try
                {
                    this.leaders = AdapterFactory.getLeaderAdapter().getLeaders();
                }
                catch
                {
                    throw;
                }
            }

            return this.leaders;
        }
    }
}
