using activity.api.Mapper;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace activity.api.Extensions
{
    public static class ServiceNugetExtesion
    {
        public static void ExtendServicesByNugets(this IServiceCollection services )
        {

           services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
           services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
           services.AddAutoMapper(typeof(MappingProfile));
           services.AddSwaggerGen();

        }
    }
}
