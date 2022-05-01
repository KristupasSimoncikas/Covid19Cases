using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Teltonika.DataModels;

namespace Teltonika.Interfaces
{
    public interface IContext
    {
        DbSet<Covid19Case>? Covid19Cases { get; set; }
        DbSet<User>? Users { get; set; }
        Task<int> SaveChanges();
    }
}
