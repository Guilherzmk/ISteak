using ISteak.Commons.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Products
{
    public interface IProductService : IResultService
    {
        Task<Product> CreateAsync(Product createParams);
        Task<Product> UpdateAsync(Guid id, Product @params);
        Task<Product> GetAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
