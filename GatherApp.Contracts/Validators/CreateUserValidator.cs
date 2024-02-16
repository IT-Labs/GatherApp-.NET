using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;

namespace GatherApp.Contracts.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator() 
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

            // Password regex check if contains lowercase, uppercase, numbers, special chars + between 8 and 32 characters
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Errors.NotEmpty)
                .MinimumLength(8).WithMessage(Errors.PasswordMinLength)
                .MaximumLength(32).WithMessage(Errors.PasswordMaxLength)
                .Matches(expression: Regexes.PasswordExp).WithMessage(Errors.Password);
        }
    }
}
