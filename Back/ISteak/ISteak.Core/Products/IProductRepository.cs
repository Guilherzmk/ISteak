using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Products
{
    public interface IProductRepository
    {
        Task<Product> InsertAsync(Product customer);
        Task<Product> UpdateAsync(Product customer);
        Task<long> DeleteAsync(Guid id);
        Task<Product> GetAsync(Guid id);
        Task<Product> GetAllAsync();
    }
}
