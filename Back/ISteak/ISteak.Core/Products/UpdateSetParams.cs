using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Products
{
    public static class UpdateSetParams
    {
        public static async Task<Product> SetParamsAsync(this Product _this, Product product)
        {
            if (product.Name != null)
                _this.Name = product.Name;

            if(product.Note != null)
                _this.Note = product.Note;

            if(product.Price != null)
                _this.Price = product.Price;

            if(product.StatusCode != null)
                _this.StatusCode = product.StatusCode;

            if(product.StatusName != null)
                _this.StatusName = product.StatusName;

            if(product.Quantity != null)
                _this.Quantity = product.Quantity;

            if(product.Image != null)
                _this.Image = product.Image;

            return _this;
        }
    }
}
