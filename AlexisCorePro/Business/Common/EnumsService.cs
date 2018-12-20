using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Domain.Constants;
using AlexisCorePro.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using static AlexisCorePro.Infrastructure.Extensions.EnumExtensions;

namespace AlexisCorePro.Business.Common
{
    public class EnumsService
    {
        public List<KeyValueItem> GetEnumItems<TEnum>()
        {
            var enums = Enum.GetValues(typeof(TEnum)).OfType<TEnum>();

            return enums.Select(item => new KeyValueItem
            {
                Id = item.ToString(),
                Name = GetEnumDescription(item)
            }).ToList();
        }
    }
}
