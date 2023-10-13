using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Products
{
    public static class ProductUpdate
    {
        public static async Task<Product> UpdateAsync(this Product _this, Product product)
        {

            await _this.UpdateAsync(product);

            return _this;
        }
    }
}
