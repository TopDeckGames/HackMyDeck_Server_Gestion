using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class UserEnhancement
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Enhancement_id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Boolean Unlocked { get; set; }
        public int On_current_research { get; set; }
    }
}
