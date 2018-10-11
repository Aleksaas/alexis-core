using DelegateDecompiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexisCorePro.Domain.Model
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public ICollection<Ship> Ships { get; set; }

        public bool Blacklisted { get; set; }

        [NotMapped]
        [Computed]
        public virtual int ShipNumber => Ships.Count;
    }
}
