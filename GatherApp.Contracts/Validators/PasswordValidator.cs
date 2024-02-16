using FluentValidation;
using GatherApp.Contracts.Requests;
using static GatherApp.Contracts.Constants.Errors;

namespace GatherApp.Contracts.Validators
{
    public class PasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public PasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                            .NotEmpty().WithMessage(NotEmpty)
                            .MinimumLength(8).WithMessage(PasswordMinLength)
                            .MaximumLength(32).WithMessage(PasswordMaxLength)
                            .Matches(expression: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$").WithMessage(Password);
            RuleFor(x => x.ConfirmPassword)
                            .NotEmpty().WithMessage(NotEmpty)
                            .MinimumLength(8).WithMessage(PasswordMinLength)
                            .MaximumLength(32).WithMessage(PasswordMaxLength)
                            .Matches(expression: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$").WithMessage(Password);
            RuleFor(x => x.OldPassword)
                            .NotEmpty().WithMessage(NotEmpty)
                            .MinimumLength(8).WithMessage(PasswordMinLength)
                            .MaximumLength(32).WithMessage(PasswordMaxLength)
                            .Matches(expression: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$").WithMessage(Password);
        }
    }
}
