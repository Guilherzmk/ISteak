using ISteak.Commons.Results;
using ISteak.Core.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Services.Stores
{
    public class StoreService : ResultService, IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public Task<Store> CreateAsync(Store store)
        {
            throw new NotImplementedException();
        }

        public Task<Store> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Store> GetAsync(Guid id)
        {
            var store = await _storeRepository.GetAsync(id);

            return store;
        }

        public async Task<Store> UpdateAsync(Guid id, Store @params)
        {
            var model = await this._storeRepository.GetAsync(id);

            await model.UpdateAsync(@params);

            await this._storeRepository.UpdateAsync(model);

            return model;
        }
    }
}
