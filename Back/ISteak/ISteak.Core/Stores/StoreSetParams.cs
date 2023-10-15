using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Stores
{
    public static class StoreSetParams
    {
        public static async Task<Store> SetParamsAsync(this Store _this, Store store)
        {
            if(store.Name != null)
                _this.Name = store.Name;

            if(store.Nickname != null)
                _this.Nickname = store.Nickname;

            if(store.Identity != null)
                _this.Identity = store.Identity;

            return _this;
        }
    }
}
