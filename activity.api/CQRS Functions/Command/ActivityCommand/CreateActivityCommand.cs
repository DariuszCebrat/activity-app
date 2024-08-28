using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using AutoMapper;
using MediatR;

namespace activity.api.CQRS_Functions.Command.ActivityCommand
{
    public class CreateActivityCommand
    {
        public record Request(string Title, DateTime Date, string Description, string Category, string City, string Venue) : IRequest<Response>;
        public class Handler : IRequestHandler<Request,Response>
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
                var activity = _mapper.Map<Activity>(request);
                var id = await _repository.CreateAsync(activity);
                if (id.GetType() == typeof(Guid))
                    return new Response((Guid)id);
                else
                    throw new Exception("can't parse Activity Id in  CreateActivityCommandHandler");

            }
        }
        public record Response(Guid Id);
    }
   
}
