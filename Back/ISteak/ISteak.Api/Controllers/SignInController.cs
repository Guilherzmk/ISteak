using ISteak.Core.SignIns;
using ISteak.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ISteak.Api.Controllers
{
    [Route("v1/sign-in")]
    public class SignInController : ControllerBase
    {
        private readonly IUserService userService;

        public SignInController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] SignInParams @params)
        {
            var result = await userService.LoginAsync(@params);

            if (userService.HasErrors())
            {
                var sb = new StringBuilder();
                foreach (var error in userService.Errors)
                {
                    sb.AppendLine(error.Text);
                }
                return BadRequest("erro");
                throw new Exception(sb.ToString());
            }

            return this.Ok(result);
        }
    }
}
