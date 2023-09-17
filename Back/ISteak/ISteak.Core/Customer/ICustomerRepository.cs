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
    }
}
