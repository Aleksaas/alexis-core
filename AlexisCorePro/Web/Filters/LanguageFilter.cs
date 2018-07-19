using System.Globalization;
using AlexisCorePro.Infrastructure.Helpers;
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

            CultureInfo.CurrentUICulture = new CultureInfo(CultureHelper.GetCulture(acceptLanguage));
        }
    }

}
