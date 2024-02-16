using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Validators
{
    public class CreateUserSSOValidator : AbstractValidator<CreateUserSSORequest>
    {
        public CreateUserSSOValidator()
        {
            // FirstName & LastName regex checks if alphabet + whitespace only
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(Errors.NotEmpty)
                .MaximumLength(80).WithMessage(Errors.FieldMaxLength80)
                .MinimumLength(2).WithMessage(Errors.FieldMinLength2)
                .Matches(expression: Regexes.NamesExp).WithMessage(Errors.LettersOnly);
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(Errors.NotEmpty)
                .MaximumLength(80).WithMessage(Errors.FieldMaxLength80)
                .MinimumLength(2).WithMessage(Errors.FieldMinLength2)
                .Matches(expression: Regexes.NamesExp).WithMessage(Errors.LettersOnly);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Errors.NotEmpty)
                .MaximumLength(80).WithMessage(Errors.EmailMaxLength)
                .Matches(expression: Regexes.EmailExp).WithMessage(Errors.InvalidEmail);

        }
    }
}
