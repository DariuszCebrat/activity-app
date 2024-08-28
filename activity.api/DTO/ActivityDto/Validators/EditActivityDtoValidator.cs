using activity.api.CQRS_Functions.Command.ActivityCommand;
using FluentValidation;

namespace activity.api.DTO.ActivityDto.Validators
{
    public class EditActivityDtoValidator : AbstractValidator<EditActivityDto>
    {
        public EditActivityDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
               .MaximumLength(100);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.City).NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Venue).NotEmpty()
                .MaximumLength(100);
        }
    }
}
