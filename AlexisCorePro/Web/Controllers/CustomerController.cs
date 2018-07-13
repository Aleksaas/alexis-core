using System.Collections.Generic;
using System.Threading.Tasks;
using AlexisCorePro.Business.Customers;
using AlexisCorePro.Domain;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlexisCorePro.Controllers
{
    [Route("api/customers")]
    public class CustomerController : BaseController
    {
        public CustomerController(DatabaseContext ctx) : base(ctx)
        {

        }

        // GET api/customers
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> Get()
        {
            return await ctx.Customers.ProjectTo<CustomerDto>().DecompileAsync().ToListAsync();
        }
    }
}