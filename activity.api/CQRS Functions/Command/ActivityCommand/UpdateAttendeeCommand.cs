using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using activity.domain.Interfaces.Services;
using activity.infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace activity.api.CQRS_Functions.Command.ActivityCommand
{
    public class UpdateAttendeeCommand
    {
        public record Request(Guid id): IRequest;
        public class Handler : IRequestHandler<Request>
        {
            private readonly IRepositoryBase<Activity> _repository;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(IRepositoryBase<Activity> repository, UserManager<AppUser> userManager,IUserAccessor userAccessor)
            {
                _repository = repository;
               _userManager = userManager;
                _userAccessor = userAccessor;
            }
            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var activity = await _repository.GetAll()
                     .Include(a => a.Attendies).ThenInclude(u => u.AppUser)
                     .SingleOrDefaultAsync(x => x.Id == request.id);
               
                if (activity == null) throw new NotFoundException("Could not found activity");

                var user = await _userManager.FindByNameAsync(_userAccessor.GetUserName());

                if (user == null) throw new UnauthorizedException();

                var hostUserName = activity.Attendies.FirstOrDefault(x=>x.IsHost)?.AppUser.UserName;
                var attendance = activity.Attendies.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (attendance != null && hostUserName == user.UserName)
                    activity.IsCancelled = !activity.IsCancelled;
                    
                if(attendance!=null && hostUserName!= user.UserName)
                    activity.Attendies.Remove(attendance);

                if(attendance == null)
                {
                    attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost= false
                    };
                    activity.Attendies.Add(attendance);
                }

              await _repository.UpdateAsync(activity);
            }
        }
    }
}
