using System.Collections.Generic;
using System.Threading.Tasks;
using Teltonika.DataModels;

namespace Teltonika.Interfaces
{
    public interface IUserService
    {
        Task Save(User user);
        Task<User> Get(int id);
        Task<User> GetByUsernamePassword(User user);
        Task<User> GetByUsername(string username);
        Task<string> Delete(User user);
    }
}
