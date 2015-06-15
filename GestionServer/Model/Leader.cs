using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Leader
    {
        public const int NAME_LENGTH = 50;
        public const int DESCRIPTION_LENGTH = 255;
        public const int EFFECT_LENGTH = 50;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public int Energy { get; set; }
        public int Price { get; set; }
    }
}
