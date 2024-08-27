using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using activity.infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace activity.api.Controllers
{

    public class ActivitiesController : BaseApiController
    {
        private readonly IRepositoryBase<Activity> _activityRepository;
        public ActivitiesController(IRepositoryBase<Activity> activityRepository)
        {
            _activityRepository = _activityRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return Ok(await _activityRepository.GetAll().ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);
            return Ok(activity);
        }

    }
}
