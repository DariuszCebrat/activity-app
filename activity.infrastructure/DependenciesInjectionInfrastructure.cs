using activity.domain.Entities;
using activity.domain.Interfaces.Repository;
using activity.infrastructure.Middleware;
using activity.infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity.infrastructure
{
    public static class DependenciesInjectionInfrastructure
    {
        public static void GetInfrastuctureServices(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IRepositoryBase<Activity>, RepositoryBase<Activity>>();
            services.AddScoped<IRepositoryBase<ActivityAttendee>, RepositoryBase<ActivityAttendee>>();
            services.AddScoped<IRepositoryBase<AppUser>,RepositoryBase<AppUser>>();
        }

    }
}
