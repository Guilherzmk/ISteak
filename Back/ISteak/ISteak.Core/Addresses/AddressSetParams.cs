using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Addresses
{
    public static class AddressSetParams
    {
        public static async Task<Address> SetParamsAsync(this Address _this, Address address)
        {
            if(address.Street != null) 
                _this.Street = address.Street;

            if(address.Number != null)
                _this.Number = address.Number;

            if(address.Neighborhood != null)
                _this.Neighborhood = address.Neighborhood;

            if(address.City != null)
                _this.City = address.City;

            if(address.State != null)
                _this.State = address.State;

            if(address.Complement != null)
                _this.Complement = address.Complement;

            if(address.ZipCode != null)
                _this.ZipCode = address.ZipCode;

            return _this;
        }
    }
}
