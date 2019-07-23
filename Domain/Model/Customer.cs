using DelegateDecompiler;
using Innofactor.EfCoreJsonValueConverter;
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

        public bool IsSystem { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string SapId { get; set; }

        public bool OwsIntegrationEnabled { get; set; }

        [JsonField]
        public string AssetSettings { get; set; }

        public virtual List<User> Users { get; set; }

        [NotMapped]
        [Computed]
        public virtual int ShipNumber => Ships.Count;
    }
}
