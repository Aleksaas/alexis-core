using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Business.Ships.Commands;
using AlexisCorePro.Domain;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AlexisCorePro.Business.Ships.ShipScopes;

namespace AlexisCorePro.Controllers
{
    [Route("api/ships")]
    public class ShipController : BaseController
    {
        private ShipService shipService;

        public ShipController(DatabaseContext ctx) : base(ctx)
        {
            shipService = new ShipService(ctx);
        }

        // POST api/ships/search
        [Route("/api/ships/search")]
        [HttpPost]
        public async Task<Response<SearchResponse<ShipDto>>> Search([FromBody]SearchRequest<ShipQuery> request)
        {
            var result = await shipService.Search<ShipQuery, ShipDto>(request);

            return OkResponse(result);
        }

        // GET api/ships/5
        [HttpGet("{id}")]
        public async Task<Response<ShipDto>> Get(int id)
        {
            var result = await ctx.Ships
                .ProjectTo<ShipDto>()
                .DecompileAsync()
                .FirstOrDefaultAsync(s => s.Id == id);

            return OkResponse(result);
        }

        // POST api/ships
        [HttpPost]
        public async Task<Response<ShipDto>> Post([FromBody]ShipCommand cmd)
        {
            var ship = await shipService.Create(cmd);

            return await Get(ship.Id);
        }

        // PUT api/ships/5
        [HttpPut("{id}")]
        public async Task<Response<ShipDto>> Put(int id, [FromBody]ShipCommand cmd)
        {
            var ship = await shipService.Update(id, cmd);

            return await Get(ship.Id);
        }

        // DELETE api/ships/5
        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete(int id)
        {
            await shipService.Delete(id);

            return OkResponse(true);
        }

        // GET api/customers/1/ships
        [Route("/api/customers/{id}/ships")]
        [HttpGet]
        public async Task<IEnumerable<ShipDto>> GetShipsForCustomer(int id)
        {
            return await ctx.Ships
                .Where(s => s.CustomerId == id)
                .ProjectTo<ShipDto>().DecompileAsync().ToListAsync();
        }
    }
}
