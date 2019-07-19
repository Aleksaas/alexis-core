using AlexisCorePro.Business.Common;
using AlexisCorePro.Business.Customers.Query;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Customers
{
    public class CustomerService : BaseService<Customer>
    {
        public CustomerService(DatabaseContext ctx) : base(ctx.Customers, ctx)
        {
        }

        public override IQueryable<Customer> AddSearchFilter<T>(IQueryable<Customer> model, T query)
        {
            var customerQuery = query as CustomerQuery;

            model = customerQuery.Id != null ? model.Where(e => e.Id == customerQuery.Id) : model;
            model = customerQuery.Name != null ? model.Where(e => e.Name.StartsWith(customerQuery.Name)) : model;

            return model;
        }
    }
}
