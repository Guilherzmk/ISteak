using ISteak.Commons.Results;
using ISteak.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Services.Products
{
    public class ProductService : ResultService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Product @params)
        {
            try
            {
                var product = new Product();

                product = @params;

                await _productRepository.InsertAsync(product);

                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Guid id, Product @params)
        {
            throw new NotImplementedException();
        }
    }
}
