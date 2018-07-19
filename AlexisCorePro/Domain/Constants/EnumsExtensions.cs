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
    }
}
