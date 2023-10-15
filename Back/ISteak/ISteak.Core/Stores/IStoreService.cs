using ISteak.Commons.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Stores
{
    public interface IStoreService : IResultService
    {
        Task<Store> CreateAsync(Store store);
        Task<Store> UpdateAsync(Guid id, Store @params);
        Task<Store> GetAsync(Guid id);
        Task<Store> DeleteAsync(Guid id);
        
    }
}
