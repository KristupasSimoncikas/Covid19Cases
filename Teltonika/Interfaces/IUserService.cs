using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.DataModels;

namespace Teltonika.Interfaces
{
    public interface IUserService
    {
        Task Save(User user);
        Task<User> Get(int id);
        Task<List<User>> Gets();
        Task<User> GetByUsernamePassword(User user);
        Task<string> Delete(User user);
    }
}
