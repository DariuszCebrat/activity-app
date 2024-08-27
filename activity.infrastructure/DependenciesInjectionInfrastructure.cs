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
        public static IServiceCollection GetInfrastuctureServices(this IServiceCollection services)
        {
           return services.AddScoped<ErrorHandlingMiddleware>()
                .AddScoped<IRepositoryBase<Activity>,RepositoryBase<Activity>>();
        }

    }
}
