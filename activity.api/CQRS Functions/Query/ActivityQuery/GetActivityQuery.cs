using activity.api.DTO.ActivityDto;
using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using activity.infrastructure.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace activity.api.CQRS_Functions.Query.ActivityQuery
{
    public class GetActivityQuery
    {
        public record Request(Guid id) : IRequest<Response>;
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepositoryBase<Activity> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepositoryBase<Activity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var activity = await _repository.GetAll().ProjectTo<ActivityDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x=>x.Id == request.id);
                if (activity is null)
                    throw new BadRequestException();

                return new Response(activity);
            }
        }
        public record Response(ActivityDto activity);
    }

}
