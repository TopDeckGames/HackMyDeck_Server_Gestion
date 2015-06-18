using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class UserStructure
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int structure_id { get; set; }
        public DateTime created { get; set; }
        public int level { get; set; }
        public Boolean Locked { get; set; }
        public int Effectif { get; set; }
    }
}
