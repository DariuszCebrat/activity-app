using activity.api.Mapper;
using activity.infrastructure.Photos;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace activity.api.Extensions
{
    public static class ServiceNugetExtesion
    {
        public static void ExtendServicesByNugets(this IServiceCollection services, IConfiguration config)
        {

           services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
           services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
           services.AddAutoMapper(typeof(MappingProfile));
           services.AddSwaggerGen();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

        }
    }
}
