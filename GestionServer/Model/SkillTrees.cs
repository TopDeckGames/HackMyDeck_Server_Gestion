using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class SkillTrees
    {
        public const int LABEL_LENGTH = 50;

        public int Id { get; set; }
        public string Label { get; set; }
        public int Type { get; set; }

    }
}
