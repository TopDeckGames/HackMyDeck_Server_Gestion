using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Structure
    {
        public enum TypeBat { Research=1, Store=2, Communication=3, Construction=4, Formation=5 }
        public string Name { get; set; }
        public TypeBat Type { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public Boolean Locked { get; set; }
        public int Effectif { get; set; }

        public Structure(string name, TypeBat type, int level, string description, Boolean locked, int effectif)
        {
            this.Name = name;
            this.Type = type;
            this.Level = level;
            this.Description = description;
            this.Locked = locked;
            this.Effectif = effectif;
        }
    }
}
