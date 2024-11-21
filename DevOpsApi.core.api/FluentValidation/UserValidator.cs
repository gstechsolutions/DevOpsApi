using DevOpsApi.core.api.Data.Entities;
using DevOpsApi.core.api.Models.Auth;
using FluentValidation;

namespace DevOpsApi.core.api.FluentValidation
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        { 
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(150).WithMessage("User name must not exceed 150 characters.");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.");

            RuleFor(user => user.RoleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .GreaterThan(0).WithMessage("RoleId must be greater than zero.");
        }
    }
}
