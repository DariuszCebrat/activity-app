using activity.api.DTO.IdentityDto;
using activity.domain.Entities;
using activity.domain.Interfaces.Services;
using activity.infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace activity.api.CQRS_Functions.Query.AccountQuery
{
    public class GetUserQuery
    {
        public record Request(ClaimsPrincipal userClaims):IRequest<Response>;
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly ITokenService _tokenService;

            public Handler(UserManager<AppUser> userManager,ITokenService tokenService)
            {
                _userManager = userManager;
                _tokenService = tokenService;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if(string.IsNullOrEmpty(request.userClaims.FindFirstValue(ClaimTypes.Email)))
                    throw new UnauthorizedException("Token nie posiada danych");


                var user = await _userManager.FindByEmailAsync(request.userClaims.FindFirstValue(ClaimTypes.Email));
                if (user is null)
                    throw new UnauthorizedException("Token posiada przestarzałe lub nieprawidłowe dane");

                return new Response(new UserDto { 
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = _tokenService.CreateToken(user),
                     UserName = user.UserName,
                });
            }
        }

        public record Response(UserDto userDto);
    }
}
