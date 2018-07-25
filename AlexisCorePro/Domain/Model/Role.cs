using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexisCorePro.Domain.Model
{
    public class Role : IdentityRole<string>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
