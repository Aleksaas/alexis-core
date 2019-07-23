using DelegateDecompiler;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AlexisCorePro.Domain.Model
{
    public class User : IdentityUser<int>
    {
        public List<UserRole> UserRoles { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [NotMapped]
        [Computed]
        public string RolesString => 
            UserRoles.Aggregate("", (result, ur) => result + result == "" ? ur.Role.Name : "," + ur.Role.Name);

        [Computed]
        public bool HasRole(string rolename)
        {
            return UserRoles.Any(r => r.Role.Name == rolename);
        }

    }
}
