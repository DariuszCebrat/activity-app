using activity.infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace activity.api.DTO.IdentityDto.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator(DataContext context)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Custom((userName, y) => {
                    if (context.Users.AnyAsync(u => u.UserName == userName).Result)
                        y.AddFailure("User with this UserName exist");
                    });
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((email, y) =>
                {
                    if (context.Users.AnyAsync(u => u.Email == email).Result )
                         y.AddFailure("User with this Email exist");
                });
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$").WithMessage("Must be complex!");
            RuleFor(x => x.DisplayName)
                .NotEmpty();
        }
    }
}
