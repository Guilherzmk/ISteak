using ISteak.Core.Customer;
using ISteak.Core.Users;
using ISteak.Services.Customers;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] Customer updateParams)
        {
            var customer = await _customerService.UpdateAsync(id, updateParams);

            if (_customerService.HasErrors())
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



        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var customer = await _customerService.GetAsync(id);

            if (_customerService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _customerService.Errors)
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
            var customers = await _customerService.GetAllAsync();

            if (_customerService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _customerService.Errors)
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
            await _customerService.DeleteAsync(id);

            if (_customerService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _customerService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok();

        }

        [HttpGet]
        [Route("/v1/users")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await _customerService.GetAllUserAsync();

            if (_customerService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _customerService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(users);
        }

        [HttpGet]
        [Route("/v1/stars")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllStarAsync()
        {
            var stars = await _customerService.GetAllStarAsync();

            if (_customerService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _customerService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(stars);
        }

    }
}
