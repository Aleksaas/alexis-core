using AlexisCorePro.Business.Ships.Commands;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Helpers;
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
        public static IQueryable<Ship> Search(this IQueryable<Ship> query, ShipQuery searchRequest)
        {
            var locale = CultureHelper.GetCulture(searchRequest.Locale);

            if (!string.IsNullOrEmpty(searchRequest.Name))
            {
                query = locale == CultureTwoLetterISONames.English ?
                    query.Where(e => e.Name == searchRequest.Name) :
                    query.Where(e => e.Name == searchRequest.Name);
            }

            query = searchRequest.Id != null ? query.Where(e => e.Id == searchRequest.Id) : query;
            query = searchRequest.Imd != null ? query.Where(e => e.Imd == searchRequest.Imd) : query;
            query = searchRequest.Mmsi != null ? query.Where(e => e.Mmsi == searchRequest.Mmsi) : query;

            return query;
        }

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
