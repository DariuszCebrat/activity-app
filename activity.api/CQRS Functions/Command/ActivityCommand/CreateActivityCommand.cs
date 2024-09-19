using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using activity.domain.Interfaces.Services;
using activity.infrastructure;
using activity.infrastructure.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace activity.api.CQRS_Functions.Command.ActivityCommand
{
    public class CreateActivityCommand
    {
        public record Request(string Title, DateTime Date, string Description, string Category, string City, string Venue) : IRequest<Response>;
        public class Handler : IRequestHandler<Request,Response>
        {
            private readonly IRepositoryBase<Activity> _repository;
            private readonly IRepositoryBase<ActivityAttendee> _activityAttendeeRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;

            public Handler(IRepositoryBase<Activity> repository, IMapper mapper,IUserAccessor userAccessor,UserManager<AppUser> userManager, IRepositoryBase<ActivityAttendee> activityAttendeeRepository)
            {
                _repository = repository;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _activityAttendeeRepository = activityAttendeeRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var userName = _userAccessor.GetUserName();
                if(string.IsNullOrEmpty(userName))
                    throw new UnauthorizedException("There is incorrect information in token, token could expare!");

                var user = await _userManager.FindByNameAsync(userName);
               
                if (user is null)
                    throw new UnauthorizedException("There is incorrect information in token, token could expare!");

           
                var activity = _mapper.Map<Activity>(request);
                var id = await _repository.CreateAsync(activity);
              
                if (id.GetType() == typeof(Guid) && id is not null)
                {
                    var attendee = new ActivityAttendee
                    {
                        AppUser = user,
                        AppUserId = user.Id,
                        Activity = activity,
                        ActivityId = (Guid)id,
                        IsHost = true
                    };
                    await _activityAttendeeRepository.CreateAsync(attendee);
                    return new Response((Guid)id);
                }
                else
                    throw new Exception("can't parse Activity Id in  CreateActivityCommandHandler");

            }
        }
        public record Response(Guid Id);
    }
   
}
