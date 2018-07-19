using AlexisCorePro.Domain;

namespace AlexisCorePro.Infrastructure.Helpers
{
    public static class CultureHelper
    {
        public static string GetCulture(string cultureName)
        {
            switch (cultureName)
            {
                case CultureTwoLetterISONames.English:
                    return CultureTwoLetterISONames.English;

                case CultureTwoLetterISONames.German:
                    return CultureTwoLetterISONames.German;

                default:
                    return Default.Language;
            }
        }
    }
}
