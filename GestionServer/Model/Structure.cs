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
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}
