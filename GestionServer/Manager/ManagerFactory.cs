using System;

namespace GestionServer.Manager
{
    public static class ManagerFactory
    {
        private static MasterManager masterManager;

        /// <summary>
        /// Recupère le manager du serveur maitre
        /// </summary>
        /// <returns></returns>
        public static MasterManager getMasterManager()
        {
            if(ManagerFactory.masterManager == null)
            {
                ManagerFactory.masterManager = new MasterManager();
            }
            return ManagerFactory.masterManager;
        }
    }
}