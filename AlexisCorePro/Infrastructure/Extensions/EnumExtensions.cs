using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AlexisCorePro.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].GetName();
            else
                return value.ToString();
        }

        public static string ToDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return string.Empty;
            }

            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
