﻿using System.Collections.Generic;
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
        public async Task<Response<SearchResponse<ShipDetails>>> Search([FromBody]SearchRequest<ShipQuery> request)
        {
            var result = await shipService.Search<ShipQuery, ShipDetails>(request);

            return OkResponse(result);
        }

        // GET api/ships/5
        [HttpGet("{id}")]
        public async Task<Response<ShipDetails>> Get(int id)
        {
            var result = await ctx.Ships
                .ProjectTo<ShipDetails>()
                .DecompileAsync()
                .FirstOrDefaultAsync(s => s.Id == id);

            return OkResponse(result);
        }

        // POST api/ships
        [HttpPost]
        public async Task<Response<ShipDetails>> Post([FromBody]ShipCommand cmd)
        {
            var ship = await shipService.Create(cmd);

            return await Get(ship.Id);
        }

        // PUT api/ships/5
        [HttpPut("{id}")]
        public async Task<Response<ShipDetails>> Put(int id, [FromBody]ShipCommand cmd)
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
        public async Task<IEnumerable<ShipDetails>> GetShipsForCustomer(int id)
        {
            return await ctx.Ships
                .Where(s => s.CustomerId == id)
                .ProjectTo<ShipDetails>().DecompileAsync().ToListAsync();
        }
    }
}