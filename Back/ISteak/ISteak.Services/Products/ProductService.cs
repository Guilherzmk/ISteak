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

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.DeleteAsync(id);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<Product> UpdateAsync(Guid id, Product @params)
        {
            var model = await this._productRepository.GetAsync(id);

            await model.UpdateAsync(@params);

            await this._productRepository.UpdateAsync(model);

            return model;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var model = await _productRepository.GetAllAsync();

            return model;
        }

        public async Task<Product> GetAsync(Guid id)
        {
            var product = await this._productRepository.GetAsync(id);

            return product;
        }
    }
}
