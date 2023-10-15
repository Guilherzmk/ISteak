using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Stores
{
    public static class StoreUpdate
    {
        public static async Task<Store> UpdateAsync(this Store _this, Store store)
        {
            await _this.SetParamsAsync(store);

            return _this;
        }
    }
}
