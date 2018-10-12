using AlexisCorePro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Customers.Validations
{
    public class CustomerValidations
    {
        private readonly DatabaseContext ctx;

        public CustomerValidations(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool IsCustomerNotBlacklisted(int customerId)
        {
            return !ctx.Customers.Where(e => e.Id == customerId).Select(e => e.Blacklisted).First();
        }
    }
}
