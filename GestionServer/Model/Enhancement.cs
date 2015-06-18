using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Enhancement
    {
        public const int NAME_LENGTH = 50;
        public const int DESCRIPTION_LENGTH = 255;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object Effect { get; set; }
        public int Cost { get; set; }
        public int Time { get; set; }
        public int SkillTree { get; set; }
        public int Parent { get; set; }
    }
}