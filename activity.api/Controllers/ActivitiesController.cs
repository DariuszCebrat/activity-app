using activity.api.CQRS_Functions.Command.ActivityCommand;
using activity.api.CQRS_Functions.Query.ActivityQuery;
using activity.api.DTO.ActivityDto;
using activity.domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace activity.api.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            var response = await Mediator.Send(new GetActivitiesQuery.Request());
            return Ok(response.activities );
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetActivityQuery.Request(id));
            return Ok(response.activity);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateActivity([FromBody] CreateActivityDto dto)
        {
            var request = Mapper.Map<CreateActivityCommand.Request>(dto);
            var response = await Mediator.Send(request);
            return Ok(response.Id);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActivity([FromRoute] Guid id, [FromBody] EditActivityDto dto)
        {
            var request = Mapper.Map<EditActivityCommand.Request>(dto);
            request.Id = id;
            await Mediator.Send(request);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAcivity([FromRoute]Guid id)
        {
            await Mediator.Send(new DeleteActivityCommand.Request(id));
            return Ok();
        }

    }
}
