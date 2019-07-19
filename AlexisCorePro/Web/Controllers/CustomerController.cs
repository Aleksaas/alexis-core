using System.Threading.Tasks;
using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Business.Customers;
using AlexisCorePro.Business.Customers.Query;
using AlexisCorePro.Domain;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexisCorePro.Controllers
{
    [Route("api/customers")]
    public class CustomerController : BaseController
    {
        private readonly CustomerService customerService;

        public CustomerController(DatabaseContext ctx, CustomerService customerService) : base(ctx)
        {
            this.customerService = customerService;
        }

        // POST api/customers/search
        [Route("/api/customers/search")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<Response<SearchResponse<CustomerDetails>>> Search([FromBody]SearchRequest<CustomerQuery> request)
        {
            var result = await customerService
                .Search(request)
                .ProjectTo<CustomerDetails>()
                .ToPaginated(request.PageNumber, request.PageSize);

            return OkResponse(result);
        }
    }
}