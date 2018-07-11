using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class Ship : BaseModel
    {
        public string Name { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
