using AlexisCorePro.Business.Common.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Ships.Commands
{
    public class ShipQuery : BaseQuery
    {
        public int? Id { get; set; }
    }
}
