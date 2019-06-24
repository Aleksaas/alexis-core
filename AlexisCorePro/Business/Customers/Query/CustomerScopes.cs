using AlexisCorePro.Business.Companies;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Domain.Model;
using System.Collections.Generic;

namespace AlexisCorePro.Business.Customers
{
    public class CustomerBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // public List<ShipDetails> Ships { get; set; } Example of how circular reference is handled automatically
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
