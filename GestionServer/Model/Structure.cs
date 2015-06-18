using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Structure
    {
        public enum TypeBat { Research = 1, Store = 2, HumanRessources = 3 }

        public const int NAME_LENGTH = 50;
        public const int DESCRIPTION_LENGTH = 255;

        public int Id { get; set; }
        public string Name { get; set; }
        public TypeBat Type { get; set; }
        public string Description { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
