using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using MediatR;

namespace activity.api.CQRS_Functions.Command.ActivityCommand
{
    public class DeleteActivityCommand
    {
        public record Request(Guid Id):IRequest;

        public class Handler : IRequestHandler<Request>
        {
            private readonly IRepositoryBase<Activity> _repository;

            public Handler(IRepositoryBase<Activity> repository)
            {
                _repository = repository;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                await _repository.DeleteAsync(request.Id);
            }
        }
    }
}
