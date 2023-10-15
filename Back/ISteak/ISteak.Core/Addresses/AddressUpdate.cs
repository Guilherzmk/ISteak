using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Addresses
{
    public static class AddressUpdate
    {
        public static async Task<Address> UpdateAsync(this Address _this, Address address)
        {
            await _this.SetParamsAsync(address);
            
            return _this;
        }  
    }
}
