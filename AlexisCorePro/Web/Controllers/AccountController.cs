using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AlexisCorePro.Business.Auth.Command;
using AlexisCorePro.Business.Users;
using AlexisCorePro.Controllers;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlexisCorePro.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly LoginCommandValidator loginCmdValidator;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            DatabaseContext ctx,
            LoginCommandValidator loginCmdValidator
            ) : base(ctx)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.loginCmdValidator = loginCmdValidator;
        }

        [Route("/api/account/login")]
        [HttpPost]
        public object Login([FromBody] LoginCommand cmd)
        {
            cmd.Validate<LoginCommand, LoginCommandValidator>(loginCmdValidator);

            var appUser = ctx.Users.IncludeAll().First(r => r.Email == cmd.Email);

            return OkResponse(SecurityHelper.GenerateJwtToken(cmd.Email, appUser));
        }

        [Route("/api/account/register")]
        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                var createdUser = ctx.Users.IncludeAll().SingleOrDefault(r => r.Email == model.Email);

                return OkResponse(SecurityHelper.GenerateJwtToken(model.Email, createdUser));
            }

            throw new Exception("UNKNOWN_ERROR");
        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}