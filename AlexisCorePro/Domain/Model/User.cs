using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AlexisCorePro.Domain.Model
{
    public class User : IdentityUser<int>
    {
        public List<UserRole> UserRoles { get; set; }

        [NotMapped]
        public string RolesString => 
            UserRoles.Aggregate("", (result, ur) => result + result == "" ? ur.Role.Name : "," + ur.Role.Name);

    }
}
