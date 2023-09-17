using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ISteak.Commons.Results;
using ISteak.Core.Customer;


namespace ISteak.Services.Customers
{
    public class CustomerService : ResultService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateAsync(Customer @params)
        {
            try
            {
                var customer = await this._customerRepository.InsertAsync(@params);

                return customer;

            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
