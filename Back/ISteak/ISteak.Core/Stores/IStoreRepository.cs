using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Stores
{
    public interface IStoreRepository
    {
        Task<Store> InsertAsync(Store store);
        Task<Store> UpdateAsync(Store store);
        Task<long> DeleteAsync(Guid id);
        Task<Store> GetAsync(Guid id);
        
    }
}
