using activity.api.DTO.PhotoDto;
using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using activity.domain.Interfaces.Services;
using activity.infrastructure;
using activity.infrastructure.Exceptions;
using activity.infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace activity.api.CQRS_Functions.Command.PhotoCommand
{
    public class CreatePhotoCommand
    {
        public record Request(IFormFile File):IRequest<Response>;
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepositoryBase<AppUser> _userRepository;
            private readonly IPhotoAccessorService _photoAccessorService;
            private readonly IUserAccessor _userAccessor;

            public Handler(IRepositoryBase<AppUser> userRepository, IPhotoAccessorService photoAccessorService,IUserAccessor userAccessor)
            {
                _userRepository = userRepository;
                _photoAccessorService = photoAccessorService;
                _userAccessor = userAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAll().Include(x => x.Photos)
                    .FirstOrDefaultAsync(x=>x.UserName == _userAccessor.GetUserName());
                
                if (user == null) throw new UnauthorizedException("Could not get user data");

                var photoUploadResult = await _photoAccessorService.AddPhoto(request.File);
                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                };
                if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;
                user.Photos.Add(photo);
                await _userRepository.UpdateAsync(user);
                return new Response(new PhotoDto
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    IsMain = photo.IsMain
                });
            }
        }
        public record Response(PhotoDto PhotoDto);
    }
}
