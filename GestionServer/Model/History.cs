using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class History
    {
        public int Id { get; set; }
        public int FirstToPlay_id { get; set; }
        public DateTime Created { get; set; }
        public int TotalDamage { get; set; }
        public int TotalUnit { get; set; }
        public int TotalTechno { get; set; }
        public int Winner { get; set; }
        public int SecondToPlay_id { get; set; }
    }
}
