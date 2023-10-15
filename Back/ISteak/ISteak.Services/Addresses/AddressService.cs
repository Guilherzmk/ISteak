using ISteak.Commons.Results;
using ISteak.Core.Addresses;
using ISteak.Core.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Services.Addresses
{
    public class AddressService : ResultService, IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> GetAsync(Guid id)
        {
            var address = await _addressRepository.GetAsync(id);

            return address;
        }

        public async Task<Address> UpdateAsync(Guid id, Address @params)
        {
            var model = await this._addressRepository.GetAsync(id);

            await model.UpdateAsync(@params);

            await this._addressRepository.UpdateAsync(model);

            return model;
        }
    }
}
