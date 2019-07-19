using AlexisCorePro.Domain.Model;
using System.Linq;

namespace AlexisCorePro.Business.Customers.Query
{
    public static class CustomerQueryExtensions
    {
        public static IQueryable<Customer> Search(this IQueryable<Customer> query, CustomerQuery searchRequest)
        {
            query = searchRequest.Id != null ? query.Where(e => e.Id == searchRequest.Id) : query;
            query = searchRequest.Name != null ? query.Where(e => e.Name.StartsWith(searchRequest.Name)) : query;

            return query;
        }
    }
}
