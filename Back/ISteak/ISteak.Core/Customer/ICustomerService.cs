using ISteak.Commons.Results;
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
    }
}
