﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreateRequest
    {
        public virtual string? Name { get; set; }
        public string? Identification { get; set; }
    }
}
