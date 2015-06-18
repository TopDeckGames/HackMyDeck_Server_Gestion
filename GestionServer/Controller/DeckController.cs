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
        public Response parser(User user, Stream stream)
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
                            response = this.getCards(user.Id);
                            break;
                        case 2:
                            response = this.getDecks(user.Id);
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

        /// <summary>
        /// Recupère l'ensemble des decks d'un utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Response</returns>
        private Response getDecks(int idUser)
        {
            List<Deck> decks = null;
            Response response = new Response();
            response.openWriter();

            try
            {
                decks = ManagerFactory.getDeckManager().getDecks(idUser);
            }
            catch(Exception e)
            {
                Logger.log(typeof(DeckManager), "Impossible de récupèrer les decks de l'utilisateur : " + e.Message, Logger.LogType.Error);
            }

            if(decks == null)
            {
                response.addValue(0);
            }
            else
            {
                response.addValue(1);

                foreach(var deck in decks)
                {
                    response.addValue(deck.Id);
                    response.addValue(deck.Leader);
                    response.addValue(deck.Cards.Count);

                    foreach(KeyValuePair<int, int> value in deck.Cards)
                    {
                        response.addValue(value.Key);
                        response.addValue(value.Value);
                    }
                }
            }

            return response;
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
