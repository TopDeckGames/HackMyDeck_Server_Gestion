using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class UserSkillTrees
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int skill_id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public Boolean unlocked { get; set; }
        public int effectif_allocated { get; set; }
        public int lastEnhancement_id { get; set; }
    }
}
