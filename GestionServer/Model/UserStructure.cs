using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class UserStructure
    {
        public int IdStructure { get; set; }
        public int Level { get; set; }
        public Boolean Locked { get; set; }
        public int Effectif { get; set; }
    }
}
