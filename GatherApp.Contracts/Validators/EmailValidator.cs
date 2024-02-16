using FluentValidation;
using static GatherApp.Contracts.Constants.Errors;
using GatherApp.Contracts.Requests;

namespace GatherApp.Contracts.Validators
{
    public class EmailValidator : AbstractValidator<EmailRequest>
    {
        public EmailValidator()
        {
            RuleFor(x => x.To)
                .NotEmpty().WithMessage(NotEmpty)
                .MaximumLength(80).WithMessage(EmailMaxLength)
                .EmailAddress().WithMessage(InvalidEmail)
                .Matches(expression: "^\\S+@it-labs\\.com$").WithMessage(InvalidEmail);
        }
    }
}
