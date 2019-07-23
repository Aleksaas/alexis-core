using AlexisCorePro.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AlexisCorePro.Infrastructure.Helpers
{
    public static class SecurityHelper
    {
        public static object GenerateJwtToken(string email, User user)
        {
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RolesString),
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.UserName),
                new Claim("Roles", JsonConvert.SerializeObject(roles)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(Startup.Configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                Startup.Configuration["JwtIssuer"],
                Startup.Configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
