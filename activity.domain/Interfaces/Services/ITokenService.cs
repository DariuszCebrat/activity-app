﻿using activity.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.domain.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
