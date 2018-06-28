using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Domain;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Common
{
    public abstract class BaseService<K> where K : class
    {
        protected DbSet<K> model;
        protected DatabaseContext ctx;

        protected BaseService(DbSet<K> model, DatabaseContext ctx)
        {
            this.model = model;
            this.ctx = ctx;
        }

        public async Task<SearchResponse<U>> Search<T, U>(SearchRequest<T> request) where T : BaseQuery
        {
            var query = model.Skip(request.PageNumber - 1).Take(request.PageSize);

            return new SearchResponse<U>
            {
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber - 1,
                Result = await AddSearchFilter(query, request.Query)
                    .ProjectTo<U>()
                    .DecompileAsync()
                    .ToListAsync()
            };
        }

        public abstract IQueryable<K> AddSearchFilter<U>(IQueryable<K> model, U query);
    }
}
