using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Requests;
using System.Text.RegularExpressions;

namespace GatherApp.Contracts.Entities
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventRequest>
    {
        public UpdateEventValidator()
        {
            RuleFor(e => e.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(20)
                .WithMessage(Errors.TitleMin)
                .MaximumLength(150)
                .WithMessage(Errors.TitleMax);

            RuleFor(e => StripHtmlTagsAndEntities(e.Description))
                .NotNull()
                .NotEmpty()
                .MinimumLength(150)
                .WithMessage(Errors.DescriptionMin)
                .MaximumLength(2000)
                .WithMessage(Errors.DescriptionMax);

            RuleFor(e => e.Category)
                .NotNull()
                .NotEmpty()
                .WithMessage(Errors.InvalidCategory);

            RuleFor(e => e.OrganizedBy)
                .NotNull()
                .NotEmpty()
                .Must(CheckOrganizedBy)
                .WithMessage(Errors.InvalidOrganizedBy);

            RuleFor(e => e.DateStart)
                .NotNull()
                .NotEmpty()
                .Must(CheckDateStart)
                .WithMessage(Errors.EarliestDate);

            RuleFor(e => e.DateEnd)
                .NotNull()
                .NotEmpty()
                .Must(CheckDateEnd)
                .WithMessage(Errors.MaximumDate);

            RuleFor(e => e.Type)
                .NotNull()
                .NotEmpty()
                .Must(CheckType)
                .WithMessage(Errors.InvalidType);

            RuleFor(e => e.LocationUrl)
                .NotNull()
                .NotEmpty()
                .Must(CheckLocationUrl)
                .WithMessage(Errors.InvalidUrl);

            RuleFor(e => e.Category)
                .NotNull()
                .NotEmpty()
                .IsEnumName(typeof(CategoryEnum))
                .WithMessage(Errors.InvalidCategory);
        }

        private bool CheckOrganizedBy(string organizedBy)
        {
            return (organizedBy == Values.OrganizedByCompany || organizedBy == Values.OrganizedByIndividual);
        }
        private bool CheckDateStart(DateTime dateStart)
        {
            return dateStart.Date >= DateTime.Now.AddDays(1).Date;
        }
        private bool CheckDateEnd(UpdateEventRequest ev, DateTime dateEnd)
        {
            return ((dateEnd.Date - ev.DateStart.Date).Days <= 60) && ((dateEnd.Date - ev.DateStart.Date).Days >= 0);
        }
        private bool CheckType(string type)
        {
            return (type == Values.TypeOnline || type == Values.TypeOnSite);
        }
        private bool CheckLocationUrl(UpdateEventRequest ev, string locationUrl)
        {
            if (!(ev.Type == Values.TypeOnline))
            {
                //maybe here we will check for valid location when we implement google maps
                return true;
            }

            //if it is online, check for valid url.
            if (!Uri.IsWellFormedUriString(locationUrl, UriKind.RelativeOrAbsolute))
            {
                return false;
            }

            return true;

        }

        private string StripHtmlTagsAndEntities(string str)
        {
            // Removes HTML entities (e.g., &lt;) and replaces them with an empty space
            var withoutEntities = Regex.Replace(str, "&[^\\s]*?;", " ");

            return Regex.Replace(withoutEntities, "<[^>]+(>|$)", "");
        }
    }
}
