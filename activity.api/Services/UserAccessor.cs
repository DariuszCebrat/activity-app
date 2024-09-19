using activity.domain.Interfaces.Services;
using System.Security.Claims;

namespace activity.api.Services
{
    public class UserAccessor: IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetUserName()
        {
            return _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        }
        public string? GetUserId()
        {
            return _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
