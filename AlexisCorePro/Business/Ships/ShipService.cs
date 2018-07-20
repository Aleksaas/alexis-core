using AlexisCorePro.Business.Common;
using AlexisCorePro.Business.Ships.Commands;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Ships
{
    public class ShipService : BaseService<Ship>
    {
        public ShipService(DatabaseContext ctx) : base(ctx.Ships, ctx)
        {

        }

        public override IQueryable<Ship> AddSearchFilter<T>(IQueryable<Ship> model, T query)
        {
            var shipQuery = query as ShipQuery;
            var locale = CultureHelper.GetCulture(shipQuery.Locale);

            if (shipQuery.Id != null)
            {
                model = model.Where(e => e.Id == shipQuery.Id);
            }

            if (!string.IsNullOrEmpty(shipQuery.Name))
            {
                // An example how would it look if we had 2 languages
                if (locale == CultureTwoLetterISONames.English)
                {
                    model = model.Where(e => e.Name == shipQuery.Name);
                }
                else
                {
                    model = model.Where(e => e.Name == shipQuery.Name);
                }
            }

            if (shipQuery.Imd != null)
            {
                model = model.Where(e => e.Imd == shipQuery.Imd);
            }

            if (shipQuery.Mmsi != null)
            {
                model = model.Where(e => e.Mmsi == shipQuery.Mmsi);
            }

            return model;
        }

        /// <summary>
        /// Creates a ship
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<Ship> Create(ShipCommand cmd)
        {
            cmd.Validate<ShipCommand, ShipCommandValidator>();

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
            cmd.Validate<ShipCommand, ShipCommandValidator>();

            var ship = await ctx.Ships.FindAsync(id);

            ship.Name = cmd.Name;
            ship.Imd = cmd.Imd;
            ship.Mmsi = cmd.Mmsi;
            ship.CustomerId = cmd.CustomerId;

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
