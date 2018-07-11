﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class Company : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
