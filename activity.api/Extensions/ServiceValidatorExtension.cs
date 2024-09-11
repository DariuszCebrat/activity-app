using activity.api.DTO.ActivityDto.Validators;
using activity.api.DTO.ActivityDto;
using FluentValidation;
using activity.api.DTO.IdentityDto;
using activity.api.DTO.IdentityDto.Validators;

namespace activity.api.Extensions
{
    public static class ServiceValidatorExtension
    {
        public static IServiceCollection GetValidators(this IServiceCollection services)
        {
            return services
                .AddScoped<IValidator<CreateActivityDto>, CreateActivityDtoValidator>()
                .AddScoped<IValidator<EditActivityDto>, EditActivityDtoValidator>()
                .AddScoped<IValidator<RegisterDto>,RegisterDtoValidator>()
                .AddScoped<IValidator<LoginDto>,LoginDtoValidator>();
        }
    }
}
