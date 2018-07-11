using DelegateDecompiler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual ICollection<Equipment> Equipments { get; set; }

        [NotMapped]
        [Computed]
        public virtual int CriticalEquipmentsNum =>
            Equipments.Where(e => e.Criticality == 2).Count();
    }
}
