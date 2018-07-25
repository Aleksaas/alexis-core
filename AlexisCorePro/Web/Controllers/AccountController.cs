using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AlexisCorePro.Business.Users;
using AlexisCorePro.Controllers;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlexisCorePro.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            DatabaseContext ctx
            ) : base(ctx)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = ctx.Users.IncludeAll().SingleOrDefault(r => r.Email == model.Email);

                return SecurityHelper.GenerateJwtToken(model.Email, appUser);
            }

            throw new Exception("INVALID_LOGIN_ATTEMPT");
        }

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

                return SecurityHelper.GenerateJwtToken(model.Email, createdUser);
            }

            throw new Exception("UNKNOWN_ERROR");
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

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