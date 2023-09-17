using ISteak.Core.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ISteak.Api.Controllers
{
    [Route("v1/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] Customer createParams)
        {
            var customer = await _customerService.CreateAsync(createParams);

            if(_customerService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _customerService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest("erro");
                throw new Exception(sb.ToString());
            }

            return this.Ok(customer);
        }

    }
}
