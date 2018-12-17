using AlexisCorePro.Domain.Enums;
using System;

namespace AlexisCorePro.Domain.Constants
{
    public static class EnumsExtensions
    {
        public static string ToDisplayName(this AisVesselType type)
        {
            switch (type)
            {
                case AisVesselType.Other:
                    return "Other";
                default:
                    throw new Exception($"Invalid value {type} for {nameof(AisVesselType)}");
            }
        }

        public static string ToDisplayName(this EquipmentCriticality enumValue)
        {
            switch (enumValue)
            {
                case EquipmentCriticality.Critical:
                    return "Critical";
                case EquipmentCriticality.Important:
                    return "Important";
                case EquipmentCriticality.Normal:
                    return "Normal";
                default:
                    throw new Exception($"Invalid value {enumValue} for {nameof(EquipmentCriticality)}");
            }
        }

        public static string ToDisplayName(this TaskOrigin enumValue)
        {
            switch (enumValue)
            {
                case TaskOrigin.Bulletin:
                    return "Bulletin";
                case TaskOrigin.ServiceReport:
                    return "Service report";
                case TaskOrigin.SmartMonitoring:
                    return "Smart monitoring";
                default:
                    throw new Exception($"Invalid value {enumValue} for {nameof(TaskOrigin)}");
            }
        }
    }
}
