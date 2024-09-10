﻿using activity.api.DTO.IdentityDto;
using activity.domain.Entities;
using activity.infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace activity.api.CQRS_Functions.Query.AccountQuery
{
    public class LoginQuery
    {
        public record Request(LoginDto loginDto) : IRequest<Response>;
        public class Handler : IRequestHandler<Request,Response>
        {
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.loginDto.Email);
                if (user is null)
                    throw new UnauthorizedException("There is no account with such email and password");

                var result = await _userManager.CheckPasswordAsync(user, request.loginDto.Password);
                if (result)
                {
                    return new Response(new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Image = null,
                        Token = "this will be token",
                        UserName = user.UserName
                    });
                    
                }
                else
                    throw new UnauthorizedException("There is no account with such email and password");
            }
        }
        public record Response(UserDto userDto);
    }
}
