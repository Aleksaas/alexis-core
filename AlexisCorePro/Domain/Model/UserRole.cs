using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexisCorePro.Domain.Model
{
    public class UserRole : IdentityUserRole<string>
    {
        public string Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
