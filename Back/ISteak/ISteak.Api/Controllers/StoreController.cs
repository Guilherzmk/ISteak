using ISteak.Core.Customer;
using ISteak.Core.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ISteak.Api.Controllers
{
    [Route("v1/stores")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService) 
        { 
            _storeService = storeService;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var store = await _storeService.GetAsync(Guid.Parse("81CB8A1F-67BA-4D55-B323-CBD16737ACAD"));

            if (_storeService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _storeService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(store);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync([FromBody] Store updateParams)
        {
            var store = await _storeService.UpdateAsync(Guid.Parse("81CB8A1F-67BA-4D55-B323-CBD16737ACAD"), updateParams);

            if (_storeService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in _storeService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest();
                throw new Exception(sb.ToString());
            }

            return this.Ok(store);
        }
    }
}
