using activity.api.CQRS_Functions.Query.AccountQuery;
using activity.api.DTO.IdentityDto;
using activity.infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace activity.api.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login ([FromBody]LoginDto loginDto)
        {
            return Ok(await Mediator.Send(new LoginQuery.Request(loginDto)));
        }
    }
}
