using ISteak.Core.Addresses;
using ISteak.Core.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ISteak.Api.Controllers
{
    [Route("v1/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var address = await _addressService.GetAsync(Guid.Parse("AF29269A-FEA7-470F-B577-C5C1DE054355"));

            if (_addressService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _addressService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(address);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync([FromBody] Address updateParams)
        {
            var address = await _addressService.UpdateAsync(Guid.Parse("AF29269A-FEA7-470F-B577-C5C1DE054355"), updateParams);

            if (_addressService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _addressService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(address);
        }
    }
}
