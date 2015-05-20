using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Card
    {
        public enum TypeRarity { grey=1, blue=2, green=3, purple=4, yellow=5}
        public enum TypeCard { virus=1, CyberWeapon=2, Firewall=3, Trojan=4, SocialEngenneering=5}

        public string Titre { get; set; }
        public string Description { get; set; }
        public int CostInGame { get; set; }
        public int CostInStore { get; set; }
        public Boolean IsBuyable { get; set; }
        public TypeCard Type { get; set; }
        public TypeRarity Rarity { get; set; }

        public Card(string titre, string description, int costInGame, int costInStore, Boolean isBuyable, TypeCard type, TypeRarity rarity)
        {
            this.Titre = titre;
            this.Description = description;
            this.CostInGame = costInGame;
            this.CostInStore = costInStore;
            this.IsBuyable = isBuyable;
            this.Type = type;
            this.Rarity = rarity;
        }
    }
}
