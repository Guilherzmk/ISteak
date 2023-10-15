using ISteak.Commons.Results;
using ISteak.Core.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Addresses
{
    public interface IAddressRepository 
    {
        Task<Address> UpdateAsync(Address address);
        Task<Address> GetAsync(Guid id);
    }
}
