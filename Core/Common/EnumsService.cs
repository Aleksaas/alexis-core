using AlexisCorePro.Business.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static AlexisCorePro.Infrastructure.Extensions.EnumExtensions;

namespace AlexisCorePro.Business.Common
{
    public class EnumsService
    {
        public List<KeyValueItem> GetEnumItems<TEnum>() where TEnum : Enum
        {
            var enums = Enum.GetValues(typeof(TEnum)).OfType<TEnum>();

            return enums.Select(item => new KeyValueItem
            {
                Id = item.ToString(),
                Name = item.ToDisplayName()
            }).ToList();
        }
    }
}
