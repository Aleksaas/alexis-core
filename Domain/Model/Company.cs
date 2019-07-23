using System.Collections.Generic;

namespace AlexisCorePro.Domain.Model
{
    public class Company : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
