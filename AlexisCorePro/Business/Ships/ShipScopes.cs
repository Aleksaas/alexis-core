using AlexisCorePro.Business.Companies;
using AlexisCorePro.Business.Customers;

namespace AlexisCorePro.Business.Ships
{
    public class ShipBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ShipDetails : ShipBasic
    {
        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public CustomerBasic Customer { get; set; }

        public CompanyBasic Company { get; set; }

        public int CriticalEquipmentsNum { get; set; }
    }

}
