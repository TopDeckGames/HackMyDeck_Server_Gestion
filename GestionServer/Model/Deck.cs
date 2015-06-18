using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Deck
    {
        public const int NAME_LENGTH = 50;
        public const int COLOR_LENGTH = 50;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Leader { get; set; }
        public Dictionary<int, int> Cards { get; set; }
    }
}
