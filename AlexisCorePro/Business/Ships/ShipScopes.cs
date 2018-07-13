using AlexisCorePro.Business.Companies;
using AlexisCorePro.Business.Customers;

namespace AlexisCorePro.Business.Ships
{
    public class ShipBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ShipDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CriticalEquipmentsNum { get; set; }

        public CustomerBasic Customer { get; set; }

        public CompanyBasic Company { get; set; }
    }

    public class ShipSearchItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CustomerId { get; set; }
    }

}
