using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexisCorePro.Business.Ships
{
    /// <summary>
    /// Extends IQueryable<Ship> to do the mapping and return ship scopes
    /// </summary>
    public static class ShipQueryExtensions
    {
        public static IQueryable<ShipMonthReport> ToShipMonthReport(this IQueryable<Ship> query, DateTime date)
        {
            return query.Select(s => new ShipMonthReport
            {
                Id = s.Id,
                Name = s.Name,
                UpdatedEquipmentNum = s.UpdatedEquipmentNum(date),
                NewEquipmentNum = s.NewEquipmentNum
            });
        }

        /// <summary>
        /// In case we have complex query we can use this
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="date"></param>
        /// <returns></returns>
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
