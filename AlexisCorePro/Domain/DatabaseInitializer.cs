using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlexisCorePro.Domain
{
    /// <summary>
    /// Populates db with data that must exist for app to work properly
    /// </summary>
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
