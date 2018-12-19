using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlexisCorePro.Business.Common;
using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Business.Ships.Commands;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Enums;
using AlexisCorePro.Web.Filters;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;

namespace AlexisCorePro.Controllers
{
    [Route("api/ships")]
    public class ShipController : BaseController
    {
        private ShipService shipService;
        private EnumsService enumsService;

        public ShipController(DatabaseContext ctx, ShipService shipService, EnumsService enumsService) : base(ctx)
        {
            this.shipService = shipService;
            this.enumsService = enumsService;
        }

        // GET api/ships/5
        [HttpGet("{id}")]
        [SwaggerResponse(200, typeof(ShipDetails))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ResourceAuthorize("id", ResourceType.Ship)]
        public async Task<Response<ShipDetails>> Get(int id)
        {
            var result = await ctx.Ships
                .ProjectTo<ShipDetails>()
                .DecompileAsync()
                .FirstAsync(s => s.Id == id);

            return OkResponse(result);
        }

        // POST api/ships
        [HttpPost]
        [SwaggerResponse(200, typeof(ShipDetails))]
        public async Task<Response<ShipDetails>> Post([FromBody]ShipCommand cmd)
        {
            var ship = await shipService.Create(cmd);

            return await Get(ship.Id);
        }

        // PUT api/ships/5
        [HttpPut("{id}")]
        [SwaggerResponse(200, typeof(ShipDetails))]
        public async Task<Response<ShipDetails>> Put(int id, [FromBody]ShipCommand cmd)
        {
            var ship = await shipService.Update(id, cmd);

            return await Get(ship.Id);
        }

        // DELETE api/ships/5
        [HttpDelete("{id}")]
        [SwaggerResponse(200, typeof(bool))]
        public async Task<Response<bool>> Delete(int id)
        {
            await shipService.Delete(id);

            return OkResponse(true);
        }

        // GET api/ships-metadata
        [Route("/api/ships-metadata")]
        public dynamic GetMetadata()
        {
            var result = new
            {
                AisVesselTypes = enumsService.GetAisVesselTypes()
            };

            return OkResponse(result);
        }

        // POST api/ships/search
        [Route("/api/ships/search")]
        [HttpPost]
        [SwaggerResponse(200, typeof(ShipListItem))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<Response<SearchResponse<ShipListItem>>> Search([FromBody]SearchRequest<ShipQuery> request)
        {
            var result = await shipService
                .Search(request)
                .ProjectTo<ShipListItem>()
                .ToPaginated(request.PageNumber, request.PageSize);

            return OkResponse(result);
        }

        // GET api/ships/reports/month
        [Route("/api/ships/reports/month")]
        [HttpPost]
        [SwaggerResponse(200, typeof(ShipMonthReport))]
        public async Task<Response<SearchResponse<ShipMonthReport>>> SearchMonthReport([FromBody]SearchRequest<ShipQuery> request)
        {
            var result = await shipService
                .Search(request)
                .ToShipMonthReport(request.Query.MonthReportDate)
                .ToPaginated(request.PageNumber, request.PageSize);

            return OkResponse(result);
        }

        // GET api/customers/1/ships
        [Route("/api/customers/{id}/ships")]
        [HttpGet]
        [SwaggerResponse(200, typeof(ShipDetails))]
        public async Task<IEnumerable<ShipDetails>> GetShipsForCustomer(int id)
        {
            return await ctx.Ships
                .Where(s => s.CustomerId == id)
                .ProjectTo<ShipDetails>().DecompileAsync().ToListAsync();
        }
    }
}
