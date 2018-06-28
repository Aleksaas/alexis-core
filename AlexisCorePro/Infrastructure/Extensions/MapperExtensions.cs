using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Infrastructure.Extensions
{
    public static class MapperExtensions
    {
        public static Task<List<TDestination>>
            ProjectToListAsync<TDestination>(this IQueryable projectionExpression)
        {
            return projectionExpression.ProjectTo<TDestination>().DecompileAsync().ToListAsync();
        }
    }
}
