using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class UserSkillTrees
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Skill_id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Boolean Unlocked { get; set; }
        public int Effectif_allocated { get; set; }
        public int LastEnhancement_id { get; set; }
    }
}
