using ISteak.Core.Customer;
using ISteak.Core.Products;
using ISteak.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ISteak.Api.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] Product createParams)
        {
            var product = await _productService.CreateAsync(createParams);

            if (_productService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _productService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest("erro");
                throw new Exception(sb.ToString());
            }

            return this.Ok(product);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] Product updateParams)
        {
            var customer = await _productService.UpdateAsync(id, updateParams);

            if (_productService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _productService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest("erro");
                throw new Exception(sb.ToString());
            }

            return this.Ok(customer);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var customer = await _productService.GetAsync(id);

            if (_productService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _productService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(customer);

        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _productService.GetAllAsync();

            if (_productService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _productService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(customers);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _productService.DeleteAsync(id);

            if (_productService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _productService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok();

        }
    }
}
