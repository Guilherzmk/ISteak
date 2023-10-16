using ISteak.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Customer
{
    public interface ICustomerRepository
    {
        Task<Customer> InsertAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task<long> DeleteAsync(Guid id);
        Task<Customer> GetAsync(Guid id);
        Task<List<Customer>> GetAllAsync();
        Task<List<User>> GetAllUserAsync();
    }
}
