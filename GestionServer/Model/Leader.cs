using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Leader
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public int Energy { get; set; }
        public int Price { get; set; }

        public Leader(string name, string effect, string description, int energy, int price)
        {
            this.Name = name;
            this.Effect = effect;
            this.Description = description;
            this.Energy = energy;
            this.Price = price;
        }
    }
}
