using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ISteak.Commons.Results;
using ISteak.Core.Customer;
using ISteak.Core.Users;

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
                var customer = new Customer();

                customer = @params;

                await this._customerRepository.InsertAsync(customer);

                return customer;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                var customer = await _customerRepository.DeleteAsync(id);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var model = await _customerRepository.GetAllAsync();

            return model;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var model = await _customerRepository.GetAllUserAsync();

            return model;
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            var customer = await this._customerRepository.GetAsync(id);

            return customer;
        }

        public async Task<Customer> UpdateAsync(Guid id, Customer @params)
        {
            var model = await this._customerRepository.GetAsync(id);

            await model.UpdateAsync(@params);

            await this._customerRepository.UpdateAsync(model);

            return model;
        }
    }
}
