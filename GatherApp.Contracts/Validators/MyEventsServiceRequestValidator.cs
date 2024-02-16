using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;

namespace GatherApp.Contracts.Validators
{
    public class MyEventsServiceRequestValidator : AbstractValidator<MyEventsServiceRequest>
    {
        public MyEventsServiceRequestValidator()
        {
            RuleFor(o => o.Organizer)
                .Must(CheckOrganizerInput).WithMessage(Errors.InvalidOrganizedBy);
        }

        private bool CheckOrganizerInput(string organizer)
        {
            return organizer == null || Enum.IsDefined(typeof(Enums.OrganizedByEnum), organizer);
        }
    }
}
