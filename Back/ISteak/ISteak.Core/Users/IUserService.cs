using ISteak.Commons.Results;
using ISteak.Core.SignIns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Users
{
    public interface IUserService : IResultService
    {
        Task<User> CreateAsync(User createParams);
        Task<User> LoginAsync(SignInParams @params);
    }
}
