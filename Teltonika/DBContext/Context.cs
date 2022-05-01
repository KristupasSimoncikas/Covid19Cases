using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Teltonika.DataModels;
using Teltonika.Interfaces;

namespace Teltonika.DBContext
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Covid19Case>? Covid19Cases { get; set; }
        public DbSet<User>? Users { get; set; }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
