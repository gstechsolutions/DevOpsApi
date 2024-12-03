using DevOpsApi.core.api.Models.Auth;
using FluentValidation;

namespace DevOpsApi.core.api.FluentValidation
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequestModel>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(150).WithMessage("User name must not exceed 150 characters.");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.");
        }
    }
}
