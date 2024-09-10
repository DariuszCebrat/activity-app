using activity.domain.Entities;
using activity.infrastructure;

namespace activity.api.Extensions
{
    public static class ServiceIdentityExtension
    { 
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication();
            return services;
        }
    }
}
