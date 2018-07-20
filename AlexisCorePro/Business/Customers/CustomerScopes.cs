using AlexisCorePro.Business.Companies;

namespace AlexisCorePro.Business.Customers
{
    public class CustomerBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CustomerDetails : CustomerBasic
    {
        public CompanyBasic Company { get; set; }

        public int ShipNumber { get; set; }
    }

    public class CustomersSearchItem : CustomerBasic
    {

    }

}
