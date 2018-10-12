using AlexisCorePro.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlexisCorePro.Business.Users
{
    public static class UserQueryExtensions
    {
        public static IQueryable<User> IncludeAll(this IQueryable<User> query)
        {
            return query.Include("UserRoles.Role");
        }
    }
}
