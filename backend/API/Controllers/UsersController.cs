using Application.Services.Identity;
using Core.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginViaIdentityValues values)
        {
            var result = await Mediator.Send(new LoginViaIdentity.Query { Values = values });
            return HandleResult(result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterViaIdentityValues values)
        {
            var result = await Mediator.Send(new RegisterViaIdentity.Command { Values = values });
            return HandleResult(result);
        }
        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await Mediator.Send(new GetCurrentUser.Query { User = User });
            return HandleResult(result);
        }
    }
}