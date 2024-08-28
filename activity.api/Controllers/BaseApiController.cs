using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace activity.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        protected IMapper Mapper=> _mapper??= HttpContext.RequestServices.GetService<IMapper>();
        protected IMediator Mediator => _mediator??=HttpContext.RequestServices.GetService<IMediator>();
    }
}
