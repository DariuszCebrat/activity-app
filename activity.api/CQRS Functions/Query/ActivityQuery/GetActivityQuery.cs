using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using MediatR;

namespace activity.api.CQRS_Functions.Query.ActivityQuery
{
    public class GetActivityQuery
    {
        public record Request(Guid id) : IRequest<Response>;
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepositoryBase<Activity> _repository;

            public Handler(IRepositoryBase<Activity> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var activity = await _repository.GetAsync(request.id);
                return new Response(activity);
            }
        }
        public record Response(Activity activity);
    }

}
