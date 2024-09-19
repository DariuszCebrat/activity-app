using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace activity.domain.Interfaces.Services
{
    public interface IUserAccessor
    {
        string? GetUserName();

         string? GetUserId();

    }
}
