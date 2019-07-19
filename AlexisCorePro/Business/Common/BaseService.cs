using AlexisCorePro.Domain;

namespace AlexisCorePro.Business.Common
{
    public abstract class BaseService
    {
        protected DatabaseContext ctx;

        protected BaseService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
    }
}
