using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Controller
{
    public class DeckController : IController
    {
        public Response parser(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        private void createDeck(int idUser, int idLeader, string name)
        {

        }

        private void deleteDeck(int idUser, int idDeck)
        {

        }

        private void saveDeck(int idUser, int idDeck, Dictionary<int, int> cards)
        {

        }

        private void getDecks(int idUser)
        {

        }

        public void getCards(int idUser)
        {

        }
    }
}
