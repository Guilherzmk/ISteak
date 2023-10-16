using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Users
{
    public interface IUserRepository
    {
        Task<User> InsertAsync(User user);
        Task<User> Get(string accessKey);
        Task<User> Get(Guid id);
    }
}
