﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Model
{
    public class Request
    {
        public enum TypeRequest { Check = 1, Register = 2 }

        public TypeRequest Type { get; set; }
        public string Data { get; set; }
    }
}
