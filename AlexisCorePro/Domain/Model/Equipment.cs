﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class Equipment : BaseModel
    {
        public string Name { get; set; }

        public int Criticality { get; set; }

        public virtual Ship Ship { get; set; }
    }
}
