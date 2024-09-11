using activity.api.DTO.IdentityDto;
using activity.domain.Entities;
using activity.domain.Interfaces.Services;
using activity.infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace activity.api.CQRS_Functions.Command.AccountCommnd
{
    public class RegisterCommand
    {
        public record Request(RegisterDto dto):IRequest<Response>;
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
                var user = new AppUser()
                {
                    DisplayName = request.dto.DisplayName,
                    Email = request.dto.Email,
                    UserName = request.dto.UserName,

                };
                var result = await  _userManager.CreateAsync(user, request.dto.Password);
                if (result.Succeeded)
                {
                    return new Response(new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Image = null,
                        Token = _tokenService.CreateToken(user),
                        UserName = user.UserName,
                    });
                }
                else
                    throw new BadRequestException("Nie udało się zarejestrować użytkownika sprawdz czy hasło spełniło wymagania");
            }
        }

        public record Response(UserDto userDto);
    }
}
