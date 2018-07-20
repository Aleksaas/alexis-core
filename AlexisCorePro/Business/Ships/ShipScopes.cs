using AlexisCorePro.Business.Companies;
using AlexisCorePro.Business.Customers;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexisCorePro.Business.Ships
{
    public class ShipBasic
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ShipDetails : ShipBasic
    {
        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public CustomerBasic Customer { get; set; }

        public CompanyBasic Company { get; set; }

        public int CriticalEquipmentsNum { get; set; }
    }

    public class ShipMonthReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NewEquipmentNum { get; set; }
        public int UpdatedEquipmentNum { get; set; }
    }

    public static class ShipMonthReportExtensions
    {
        public static IQueryable<ShipMonthReport> ToShipMonthReport(this IQueryable<Ship> query, DateTime date)
        {
            //var range = DateHelper.GetFirstAndLastDay(DateTime.Now);

            return query.Select(s => new ShipMonthReport
            {
                Id = s.Id,
                Name = s.Name,
                //NewEquipmentNum = s.Equipments.Where(e => e.CreatedAt <= range.DateTo
                //    && e.CreatedAt >= range.DateFrom).Count(),
                UpdatedEquipmentNum = s.UpdatedEquipmentNum(date)
            });
        }
    }

}
