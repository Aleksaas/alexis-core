using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Domain.Constants;
using AlexisCorePro.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexisCorePro.Business.Common
{
    public class EnumsService
    {
        public List<KeyValueItem> GetAisVesselTypes()
        {
            var enums = Enum.GetValues(typeof(AisVesselType)).OfType<AisVesselType>();

            return enums.Select(item => new KeyValueItem
            {
                Id = item.ToString(),
                Name = item.ToDisplayName()
            }).ToList();
        }
    }
}
