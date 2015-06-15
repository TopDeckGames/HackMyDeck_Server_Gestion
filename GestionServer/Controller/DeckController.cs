using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionServer.Model;
using GestionServer.Manager;

namespace GestionServer.Controller
{
    public class DeckController : IController
    {
        /// <summary>
        /// Redirige la requête vers l'action correspondante
        /// </summary>
        /// <param name="stream">Flux de données à traiter</param>
        public Response parser(Stream stream)
        {
            Response response = null;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                try
                {
                    ushort idAction = reader.ReadUInt16();

                    switch (idAction)
                    {
                        case 1:
                            response = this.getCards(reader.ReadInt32());
                            break;
                        default:
                            Logger.log(typeof(UserController), "L'action n'existe pas : " + idAction, Logger.LogType.Error);
                            response = new Response();
                            response.addValue(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.log(typeof(UserController), e.Message, Logger.LogType.Error);
                    response = new Response();
                    response.addValue(0);
                }
            }
            return response;
        }

        private void saveDeck(int idUser, int idDeck, Dictionary<int, int> cards)
        {

        }

        private void getDecks(int idUser)
        {

        }

        /// <summary>
        /// Récupère les cartes possèdées par l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Réponse</returns>
        public Response getCards(int idUser)
        {
            Dictionary<Card, int> cards = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                cards = ManagerFactory.getCardManager().getOwnedCards(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(CardManager), "Impossible de récupèrer les cartes de l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if(cards == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach (KeyValuePair<Card, int> value in cards)
                {
                    response.addValue(value.Key.Id);
                    response.addValue(value.Value);
                }
            }

            return response;
        }
    }
}
