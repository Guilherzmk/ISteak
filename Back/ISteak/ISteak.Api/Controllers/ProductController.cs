using ISteak.Core.Customer;
using ISteak.Core.Products;
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
    }
}
