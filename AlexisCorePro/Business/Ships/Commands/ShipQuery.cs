using AlexisCorePro.Business.Common.Model.Search;
using System;

namespace AlexisCorePro.Business.Ships.Commands
{
    public class ShipQuery : BaseQuery
    {
        public string Name { get; set; }

        public int? Imd { get; set; }

        public int? Mmsi { get; set; }

        public DateTime MonthReportDate { get; set; }
    }
}
