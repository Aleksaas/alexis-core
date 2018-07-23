using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlexisCorePro.Infrastructure.Filters
{
    public class AuthenticationFilter : IActionFilter
    {
        private readonly DatabaseContext ctx;
        private readonly UserManager<User> userManager;

        public AuthenticationFilter(DatabaseContext ctx, UserManager<User> userManager)
        {
            this.ctx = ctx;
            this.userManager = userManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.User.FindFirst(e => true)?.Value;

            if (!string.IsNullOrEmpty(username))
            {
                ctx.CurrentUser = await userManager.FindByNameAsync(username);
            }
        }
    }

}
