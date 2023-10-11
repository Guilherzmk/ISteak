using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Customer
{
    public static class CustomerUpdate
    {
        public static async Task<Customer> UpdateAsync(this Customer _this, Customer customer)
        {
            await _this.SetParamsAsync(customer);

            return _this;
        }
    }
}
