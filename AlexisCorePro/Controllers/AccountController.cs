using System.Linq;
using AlexisCorePro.Business.Auth.Command;
using AlexisCorePro.Business.Users;
using AlexisCorePro.Controllers;
using AlexisCorePro.Domain;
using AlexisCorePro.Infrastructure.Extensions;
using AlexisCorePro.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AlexisCorePro.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly LoginCommandValidator _loginCmdValidator;
        private readonly RegisterCommandValidator _registerCmdValidator;

        public AccountController(
            DatabaseContext ctx,
            LoginCommandValidator loginCmdValidator,
            RegisterCommandValidator registerCmdValidator
            ) : base(ctx)
        {
            _loginCmdValidator = loginCmdValidator;
            _registerCmdValidator = registerCmdValidator;
        }

        [Route("/api/account/login")]
        [HttpPost]
        public object Login([FromBody] LoginCommand cmd)
        {
            _loginCmdValidator.ValidateCmd(cmd);

            var appUser = ctx.Users
                .IncludeRoles()
                .First(r => r.Email == cmd.Email);

            return OkResponse(SecurityHelper.GenerateJwtToken(cmd.Email, appUser));
        }

        [Route("/api/account/register")]
        [HttpPost]
        public object Register([FromBody] RegisterCommand cmd)
        {
            _registerCmdValidator.ValidateCmd(cmd);

            var createdUser = ctx.Users
                .IncludeRoles()
                .First(r => r.Email == cmd.Email);

            return OkResponse(SecurityHelper.GenerateJwtToken(cmd.Email, createdUser));
        }
    }
}