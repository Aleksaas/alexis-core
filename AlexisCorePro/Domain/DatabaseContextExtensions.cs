using AlexisCorePro.Business.Ships;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexisCorePro.Domain
{
    public static class DatabaseContextExtensions
    {
        public static IQueryable<ShipMonthReport> ToShipMonthReport(this DatabaseContext ctx, DateTime date)
        {
            var query = from s in ctx.Ships
            join eq in ctx.Equipments on s.Id equals eq.ShipId
            select new ShipMonthReport
            {
                Id = s.Id,
                NewEquipmentNum = s.NewEquipmentNum,
                UpdatedEquipmentNum = s.UpdatedEquipmentNum(date)
            };

            return query;
        }
    }
}
