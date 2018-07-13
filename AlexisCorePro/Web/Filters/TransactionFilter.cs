using System;
using System.Threading.Tasks;
using AlexisCorePro.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlexisCorePro.Infrastructure.Filters
{
    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly DatabaseContext dbContext;

        public TransactionFilter(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                dbContext.Database.BeginTransaction();

                await next();

                await dbContext.SaveChangesAsync();

                dbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                dbContext.Database.RollbackTransaction();

                throw ex;
            }
        }
    }
}
