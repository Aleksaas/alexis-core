using AlexisCorePro.Domain.Enums;
using AlexisCorePro.Infrastructure.Helpers;
using DelegateDecompiler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
            Equipments.Where(e => e.Criticality == EquipmentCriticality.Critical).Count();

        [NotMapped]
        [Computed]
        public virtual Company Company =>
            Customer.Company;

        [NotMapped]
        [Computed]
        public virtual int NewEquipmentNum
        {
            get
            {
                return Equipments.Where(e => e.IsCreatedInMonth).Count();
            }
        }

        [Computed]
        public virtual int UpdatedEquipmentNum(DateTime date)
        {
            return Equipments.Where(e => e.IsUpdatedInMonth(date)).Count();
        }
    }
}
