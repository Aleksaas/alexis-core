using static AlexisCorePro.Business.Companies.CompanyScopes;

namespace AlexisCorePro.Business.Customers
{
    public static class CustomerScopes
    {
        public class CustomerBasic
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int ShipNumber { get; set; }
        }

        public class CustomerDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public CompanyBasic Company { get; set; }

            public int ShipNumber { get; set; }
        }

        public class CustomersSearchItem
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
