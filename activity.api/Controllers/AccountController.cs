using activity.api.CQRS_Functions.Command.AccountCommnd;
using activity.api.CQRS_Functions.Query.AccountQuery;
using activity.api.DTO.IdentityDto;
using activity.domain.Entities;
using activity.infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace activity.api.Controllers
{

    public class AccountController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login ([FromBody]LoginDto loginDto)
        {
            var result = await Mediator.Send(new LoginQuery.Request(loginDto));
            return Ok(result.userDto);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto registerDto)
        {
            var result = await Mediator.Send(new RegisterCommand.Request(registerDto));
            return Ok(result.userDto);
         
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await Mediator.Send(new GetUserQuery.Request(User));
            return Ok(result.userDto);
           
        }
    }
}
