using System.Globalization;
using AlexisCorePro.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlexisCorePro.Infrastructure.Filters
{
    public class LanguageFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var hasLanguage = context.HttpContext.Request.Headers.TryGetValue("Accept-Language", out var acceptLanguage);

            CultureInfo.CurrentUICulture = hasLanguage ? new CultureInfo(acceptLanguage) : new CultureInfo(Default.Language);
        }
    }

}
