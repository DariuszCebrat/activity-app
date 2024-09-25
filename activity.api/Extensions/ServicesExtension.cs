using activity.api.Services;
using activity.domain.Interfaces.Services;

namespace activity.api.Extensions
{
    public static  class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService,TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IPhotoAccessorService, PhotoAccessorService>();
        }
    }
}
