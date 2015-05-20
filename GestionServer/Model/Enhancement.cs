using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Enhancement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object Effect { get; set; }
        public int Cost { get; set; }
        public int Time { get; set; }

        public Enhancement(string name, string description, object effect, int cost, int time)
        {
            this.Name = name;
            this.Description = description;
            this.Effect = effect;
            this.Cost = cost; 
            this.Time = time;
        }
    }
}