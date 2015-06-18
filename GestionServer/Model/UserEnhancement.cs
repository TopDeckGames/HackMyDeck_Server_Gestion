using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class UserEnhancements
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int enhancement_id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public Boolean unlocked { get; set; }
        public int on_current_research { get; set; }
    }
}
