using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Customer
{
    public static class CustomerSetParams
    {
        public static async Task<Customer> SetParamsAsync(this Customer _this, Customer customer)
        {
            if (customer.Name != null)
                _this.Name = customer.Name;

            if (customer.BirthDate != null)
                _this.BirthDate = customer.BirthDate;

            if(customer.PersonTypeCode != null)
                _this.PersonTypeCode = customer.PersonTypeCode;

            if(customer.Identity != null)
                _this.Identity = customer.Identity;

            if(customer.Phone != null)
                _this.Phone = customer.Phone;

            if(customer.Email != null)
                _this.Email = customer.Email;

            if(customer.Note != null)
                _this.Note = customer.Note;

            return _this;
        }
    }
}
