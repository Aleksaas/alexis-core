using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlexisCorePro.Business.Common
{
    public abstract class BaseService<TModel> where TModel : class
    {
        protected DbSet<TModel> model;
        protected DatabaseContext ctx;

        protected BaseService(DbSet<TModel> model, DatabaseContext ctx)
        {
            this.model = model;
            this.ctx = ctx;
        }

        public IQueryable<TModel> Search<TQuery>(SearchRequest<TQuery> request) where TQuery : BaseQuery
        {
            var query = AddSearchFilter(model, request.Query);

            return query;
        }

        public abstract IQueryable<TModel> AddSearchFilter<TQuery>(IQueryable<TModel> model, TQuery query);
    }
}
