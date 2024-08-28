using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace activity.api.CQRS_Functions.Query.ActivityQuery
{
    public class GetActivitiesQuery
    {
        public record Request() : IRequest<Response>;
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepositoryBase<Activity> _activityRepository;

            public Handler(IRepositoryBase<Activity> activityRepository)
            {
                _activityRepository = activityRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
              
                var activities = await _activityRepository.GetAll().ToListAsync();
                return new Response(activities);
            }
        }
        public record Response(IEnumerable<Activity> activities);
    }
}
