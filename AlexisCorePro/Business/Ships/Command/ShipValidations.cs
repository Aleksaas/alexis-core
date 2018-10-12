using AlexisCorePro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Ships.Validations
{
    public class ShipValidations
    {
        private readonly DatabaseContext ctx;

        public ShipValidations(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool IsNameUnique(string shipName)
        {
            return !ctx.Ships.Any(e => e.Name == shipName);
        }

        public bool IsMaxNumberReachedForCustomer(int customerId)
        {
            return ctx.Ships.Where(e => e.CustomerId == customerId).Count() < 3;
        }
    }
}
