using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlexisCorePro.Domain
{
    public class DatabaseInitializer
    {
        private DatabaseContext ctx;

        public DatabaseInitializer(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public void Initialize()
        {
            InsertPostnumbers();

            ctx.SaveChanges();
        }

        private void InsertPostnumbers()
        {
            if (ctx.Postnumbers.Count() == 0)
            {
                SeedScripts.SeedANSI("InsertPostnumbers.sql", ctx);
            }
        }
    }
}
