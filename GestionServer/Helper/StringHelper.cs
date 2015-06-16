﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Helper
{
    public static class StringHelper
    {
        private const char SEPARATOR = '*';

        /// <summary>
        /// Enlève les charactères de remplissage d'une chaine
        /// </summary>
        /// <param name="str">Chaine réelle</param>
        /// <returns></returns>
        public static string getTrueString(string str)
        {
            return str.Split(StringHelper.SEPARATOR)[0];
        }

        /// <summary>
        /// Ajoute les charactères de remplissage en fin de chaine selon la taille souhaitées
        /// </summary>
        /// <param name="str">Chaine de charactères</param>
        /// <param name="length">Longueur souhaitée</param>
        /// <returns>Chaine remplie</returns>
        public static string fillString(string str, int length)
        {
            while(str.Length < length)
            {
                str += StringHelper.SEPARATOR;
            }
            return str;
        }
    }
}
