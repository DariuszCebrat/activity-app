using activity.api.CQRS_Functions.Command.PhotoCommand;
using activity.api.DTO.PhotoDto;
using Microsoft.AspNetCore.Mvc;

namespace activity.api.Controllers
{
    public class PhotosController:BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<PhotoDto>> Add([FromForm] CreatePhotoCommand.Request request)
        {
           
            return Ok(await Mediator.Send(request) );
        }
    }
}
