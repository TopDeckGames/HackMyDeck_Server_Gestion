using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class History
    {
        public int id { get; set; }
        public int firstToPlay_id { get; set; }
        public DateTime created { get; set; }
        public int totalDamage { get; set; }
        public int totalUnit { get; set; }
        public int totalTechno { get; set; }
        public int winner { get; set; }
        public int secondToPlay_id { get; set; }
    }
}
