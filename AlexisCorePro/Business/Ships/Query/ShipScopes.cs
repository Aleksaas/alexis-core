using AlexisCorePro.Business.Companies;
using AlexisCorePro.Business.Customers;
using AlexisCorePro.Domain.Enums;
using AlexisCorePro.Infrastructure.Extensions;
using System;

namespace AlexisCorePro.Business.Ships
{
    public class ShipBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public AisVesselType AisVesselType { get; set; }

        public DateTime Date { get; set; }

        public byte[] RowVersion { get; set; }

        public int CustomerId { get; set; }

        public string AisVesselTypeLabel => AisVesselType.ToDisplayName();
    }

    public class ShipDetails : ShipBasic
    {
        public CustomerDetails Customer { get; set; }

        public int CriticalEquipmentsNum { get; set; }
    }

    public class ShipListItem : ShipBasic
    {
        public CustomerDetails Customer { get; set; }

        public int CriticalEquipmentsNum { get; set; }
    }

    public class ShipMonthReport : ShipBasic
    {
        public int NewEquipmentNum { get; set; }
        public int UpdatedEquipmentNum { get; set; }
    }
}
