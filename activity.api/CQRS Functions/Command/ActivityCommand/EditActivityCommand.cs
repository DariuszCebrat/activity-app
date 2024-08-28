using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace activity.api.CQRS_Functions.Command.ActivityCommand
{
    public class EditActivityCommand
    {
     
        public record Request:IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }
        public class Handler : IRequestHandler<Request>
        {
            private readonly IRepositoryBase<Activity> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepositoryBase<Activity> repository,IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var activity = _mapper.Map<Activity>(request);
                await _repository.UpdateAsync(activity);
            }
        }
    }
}
