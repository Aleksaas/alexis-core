using AlexisCorePro.Business.Companies;
using AlexisCorePro.Business.Customers;

namespace AlexisCorePro.Business.Ships
{
    public class ShipBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public byte[] RowVersion { get; set; }
    }

    public class ShipDetails : ShipBasic
    {
        public CustomerBasic Customer { get; set; }

        public CompanyBasic Company { get; set; }

        public int CriticalEquipmentsNum { get; set; }
    }

    public class ShipListItem : ShipBasic
    {
        public CustomerBasic Customer { get; set; }

        public CompanyBasic Company { get; set; }

        public int CriticalEquipmentsNum { get; set; }
    }

    public class ShipMonthReport : ShipBasic
    {
        public int NewEquipmentNum { get; set; }
        public int UpdatedEquipmentNum { get; set; }
    }
}
