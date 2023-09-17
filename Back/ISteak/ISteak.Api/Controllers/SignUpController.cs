using ISteak.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISteak.Api.Controllers
{
    [Route("v1/sign-up")]
    public class SignUpController : ControllerBase
    {
        private readonly IUserService userService;

        public SignUpController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] User @params)
        {
            var user = await userService.CreateAsync(@params);

            if(user == null)
                return BadRequest();

            return Ok(user);
            
        }
    }
}
