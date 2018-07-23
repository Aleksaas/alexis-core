using System.Globalization;
using AlexisCorePro.Domain;
using AlexisCorePro.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AlexisCorePro.Infrastructure.Filters
{
    public class AuthenticationFilter : IActionFilter
    {
        private DatabaseContext Context { get; }

        public AuthenticationFilter(DatabaseContext ctx)
        {
            Context = ctx;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Context.CurrentUserId = 2;
        }
    }

}
