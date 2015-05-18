using System;

namespace GestionServer.Controller
{
    public static class ControllerFactory
    {
        private static GestionController gestionController;

        /// <summary>
        /// Retourne l'instance du controlleur de gestion
        /// </summary>
        /// <returns>Controlleur</returns>
        public static GestionController getGestionController()
        {
            if(ControllerFactory.gestionController == null)
            {
                ControllerFactory.gestionController = new GestionController();
            }
            return ControllerFactory.gestionController;
        }
    }
}