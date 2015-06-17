using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class SkillTrees
    {
        public const int LABEL_LENGTH = 50;

        public int id { get; set; }
        public string label { get; set; }
        public int type { get; set; }

    }
}
