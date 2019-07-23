using AlexisCorePro.Business.Common.Model.Search;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain
{
    public static class DatabaseContextExtensions
    {
        /// <summary>
        /// Adds pagination to IQueryable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNumber">Starts from 0 when sending from FE</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<SearchResponse<TResult>> ToPaginated<TResult>(this IQueryable<TResult> query, int pageNumber, int pageSize)
        {
            pageSize = pageSize <= 0 ? Default.PageSize : pageSize;

            var resultQuery = query.Skip(pageNumber).Take(pageSize);

            return new SearchResponse<TResult>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                EntriesCount = await query.DecompileAsync().CountAsync(),
                Result = await resultQuery.DecompileAsync().ToListAsync()
            };
        }
    }
}
