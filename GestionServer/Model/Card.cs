using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Card
    {
        public const int TITLE_LENGTH = 50;
        public const int DESCRIPTION_LENGTH = 255;

        public enum TypeRarity { grey=1, blue=2, green=3, purple=4, yellow=5}
        public enum TypeCard { virus=1, CyberWeapon=2, Firewall=3, Trojan=4, SocialEngenneering=5}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CostInGame { get; set; }
        public int CostInStore { get; set; }
        public Boolean IsBuyable { get; set; }
        public TypeCard Type { get; set; }
        public TypeRarity Rarity { get; set; }

    }
}
