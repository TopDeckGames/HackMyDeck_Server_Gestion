using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Deck
    {
        public int Leader { get; set; }
        public Dictionary<int, int> Cards { get; set; }
    }
}
