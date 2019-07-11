using AlexisCorePro.Business.Common;
using AlexisCorePro.Business.Ships.Commands;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Extensions;
using AlexisCorePro.Infrastructure.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Ships
{
    public class ShipService : BaseService<Ship>
    {
        private readonly ShipCommandValidator shipCmdValidator;

        public ShipService(DatabaseContext ctx, ShipCommandValidator shipCmdValidator) : base(ctx.Ships, ctx)
        {
            this.shipCmdValidator = shipCmdValidator;
        }

        public override IQueryable<Ship> AddSearchFilter<T>(IQueryable<Ship> model, T query)
        {
            var shipQuery = query as ShipQuery;

            var locale = CultureHelper.GetCulture(shipQuery.Locale);

            if (!string.IsNullOrEmpty(shipQuery.Name))
            {
                model = locale == CultureTwoLetterISONames.English ?
                    model.Where(e => e.Name == shipQuery.Name) :
                    model.Where(e => e.Name == shipQuery.Name);
            }

            model = shipQuery.Id != null ? model.Where(e => e.Id == shipQuery.Id) : model;
            model = shipQuery.Imd != null ? model.Where(e => e.Imd == shipQuery.Imd) : model;
            model = shipQuery.Mmsi != null ? model.Where(e => e.Mmsi == shipQuery.Mmsi) : model;

            return model;
        }

        /// <summary>
        /// Creates a ship
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<Ship> Create(ShipCommand cmd)
        {
            shipCmdValidator.ValidateCmd(cmd);

            var record = ctx.Ships.Add(new Ship
            {
                Name = cmd.Name,
                Imd = cmd.Imd,
                Mmsi = cmd.Mmsi,
                CustomerId = cmd.CustomerId
            });

            await ctx.SaveChangesAsync();

            return record.Entity;
        }

        /// <summary>
        /// Updates a ship
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<Ship> Update(int id, ShipCommand cmd)
        {
            cmd.Id = id;

            shipCmdValidator.ValidateCmd(cmd);

            var ship = await ctx.Ships.FindAsync(id);

            ship.Name = cmd.Name;
            ship.Imd = cmd.Imd;
            ship.Mmsi = cmd.Mmsi;
            ship.CustomerId = cmd.CustomerId;

            ctx.Entry(ship).OriginalValues["RowVersion"] = cmd.RowVersion;

            await ctx.SaveChangesAsync();

            return ship;
        }

        /// <summary>
        /// Deletes a ship
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var ship = await ctx.Ships.FindAsync(id);

            ctx.Ships.Remove(ship);

            await ctx.SaveChangesAsync();
        }
    }
}
