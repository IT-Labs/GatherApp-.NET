using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;

namespace GatherApp.Contracts.Validators
{
    public class ListOfUsersRequestValidator : AbstractValidator<ListOfUsersRequest>
    {
        public ListOfUsersRequestValidator()
        {
            RuleFor(r => r.Role)
                .Must(CheckRoleInput).WithMessage(Errors.InvalidRole);
        }

        private bool CheckRoleInput(string role)
        {
            return role == null || Enum.IsDefined(typeof(Enums.RoleEnum), role);
        }
    }
}
