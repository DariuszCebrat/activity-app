using activity.api.DTO.ActivityDto;
using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            private readonly IMapper _mapper;

            public Handler(IRepositoryBase<Activity> activityRepository,IMapper mapper)
            {
                _activityRepository = activityRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
              
                var result = await _activityRepository.GetAll().ProjectTo<ActivityDto>(_mapper.ConfigurationProvider).ToListAsync();
                return new Response(result);
            }
        }
        public record Response(IEnumerable<ActivityDto> activities);
    }
}
