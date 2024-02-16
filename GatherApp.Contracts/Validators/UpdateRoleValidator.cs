using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;

namespace GatherApp.Contracts.Validators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator() { 
            
            RuleFor(r => r.UserId)
                .NotNull()
                .NotEmpty().WithMessage(Errors.NotEmpty);

            RuleFor(r => r.Role)
                .NotNull()
                .NotEmpty().WithMessage(Errors.NotEmpty)
                .IsEnumName(typeof (Enums.RoleEnum)).WithMessage(Errors.InvalidRole);

        }
    }
}
