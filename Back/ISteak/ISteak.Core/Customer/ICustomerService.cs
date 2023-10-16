using ISteak.Commons.Results;
using ISteak.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Customer
{
    public interface ICustomerService : IResultService
    {
        Task<Customer> CreateAsync(Customer createParams);
        Task<Customer> UpdateAsync(Guid id, Customer @params);
        Task<Customer> GetAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<List<Customer>> GetAllAsync();
        Task<List<User>> GetAllUserAsync();
        Task<List<Stars>> GetAllStarAsync();
    }
}
