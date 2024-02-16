using static System.Net.WebRequestMethods;

namespace GatherApp.Contracts.Constants
{
    public static class Errors
    {
        // general
        public const string Unauthorized = "You are not authorized.";
        public const string GeneralError = "There was an error. Please try again later.";

        // event related
        public const string TitleMin = "You can’t use less than 20 characters.";
        public const string DescriptionMin = "You can’t use less than 150 characters.";

        public const string TitleMax = "You can’t use more than 150 characters.";
        public const string DescriptionMax = "You can’t use more than 2000 characters.";

        public const string TitleIncludes = "You can only use letters and numbers.";

        public const string EarliestDate = "The earliest date you can select is the next day.";
        public const string MaximumDate = "The maximum date you can select is 60 days appart from the start date.";

        public const string InvalidOrganizedBy = "The field needs to have value of either Company or Individual.";
        public const string InvalidType = "The field needs to have value of either Online or On-Site.";
        public const string InvalidUrl = "Enter a valid URL for Online type of event.";
        public const string InvalidCategory = "You entered an Invalid category name for the event.";
        public const string InvalidStatus = "Please enter a valid status.";
        public const string InvalidRole = "Please enter a valid role.";

        public const string InvalidLocationForOnSiteEvent = "You can't enter a link for On-Site event.";
        public const string InvalidOrganizatorForUserRole = "You can't host a company event.";

        public const string FileMaxLength = "You can’t upload photo file size greater than 5MB.";
        public const string FileType = "File type not supported.";
        public const string UploadImage = "There was an error with the image. Please try again later.";
        public const string CannotChangeActiveOrPastEventBanner = "You can't change the banner of an event that is active or has already passed.";

        public const string EventNotFound = "Event not found.";

        // user related
        public const string NotEmpty = "This field is required.";
        public const string LengthRange = "This field should contain between 2 and 80 characters.";
        public const string PasswordLengthRange = "This field should contain between 8 and 32 characters.";
        public const string NameMaxLength = "This field should not contain more than 80 characters.";
        public const string NameMinLength = "This field should contain at least 2 characters.";

        public const string Password = "Your password must contain at least 8 characters, at least one number, at least one lowercase letter, at least one uppercase letter, and at least one special character.";
        public const string EmailAlreadyExists = "An account with this email address has already been registered.";
        public const string InvalidCountry = "There was an error selecting your country. Please try again later.";

        public const string UserNotFound = "User not found.";
        public const string UserNotAuthenticated = "Not Authenticated.";

        public const string InvalidEmail = "Please enter a valid email address.";
        public const string EmailMaxLength = "The Email value cannot exceed 80 characters.";
        public const string FieldMaxLength80 = "You can't use more than 80 characters.";
        public const string FieldMinLength2 = "You can't use less than 2 characters.";
        public const string LettersOnly = "You can only use letters.";
        public const string PasswordMinLength = "Your password must contain at least 8 characters.";
        public const string PasswordMaxLength = "The max length is reached.";
        public const string CurrentPasswordMismatch = "Current password is incorrect. Please enter the correct current password.";
        public const string NewPasswordSameAsOld = "New password cannot be the same as the old password. Please choose a different password.";


        public const string NoProfileImage = "User doesn't have a profile picture.";

        // Token related
        public const string TokenNotFound = "Token not found.";
        public const string TokenIsExpired = "Token has expired.";
        public const string TokenIsUsed = "Token has been used.";

        //Event invitation related
        public const string UnexpectedError = "An unexpected error occured.";
        public const string UninvitedUser = "You haven't been invited to this event.";

    }

    public static class Messages
    {
        // General
        public const string SuccessfulImageUpload = "You've successfully uploaded an image.";

        // Event related
        public const string ApprovedEvent = "The event was successfully approved.";
        public const string DeclinedEvent = "The event was successfully declined.";
        public const string NotPendingEvent = "Event status is NOT pending.";

        public const string SuccessfulSubmit = "You've successfully submited the event.";
        public const string SuccessfulEdit = "You've successfully edited the event.";
        public const string SuccessfulDelete = "You've successfully deleted the event.";

        // User related
        public const string SuccessfulLogin = "You've successfully logged in.";
        public const string SuccessfulSignUp = "You've successfully signed up.";
        public const string SuccessfulLogout = "Thanks for stopping by! Have a great day!";
        public const string SuccessfulProfilePhotoUpload = "You've successfully uploaded a new profile photo.";
        public const string SuccessfulProfilePhotoRemove = "You've successfully removed your profile photo.";
        public const string SuccessfulPasswordChange = "You've successfully changed your password.";
        public const string SuccessfulCountryUpdate = "You've successfully updated your country.";
        public const string ForgotPasswordSubmission = "We've received your submission. If you have a valid account, you will receive a reset password link via Email.";
        public const string SameRole = "The user has already been assigned the specified role.";
        public const string SuccessfulRoleUpdate = "You've successfully updated this user's role.";
        
        // Event invitation related
        public const string UserGoing = "The invite status has been updated to 'Going'.";
        public const string UserNotGoing = "The invite status has been updated to 'Not Going'.";
        public const string UserMaybeGoing = "The invite status has been updated to 'Maybe'.";

    }
        
    public static class Values {

        public const string OrganizedByCompany = "Company";
        public const string OrganizedByIndividual = "Individual";
        public const string OrganizedByBoth = "Both";

        public const string RoleFilterAll = "All";

        public const string EventStatusApproved = "Approved";
        public const string EventStatusPending = "Pending";
        public const string EventStatusDeclined = "Declined";
        public const string EventStatusPast = "Past";

        public const string TypeOnline = "Online";
        public const string TypeOnSite = "On-site";

        public const string JpegContentType = "image/jpeg";
        public const string JpgContentType = "image/jpg";
        public const string PngContentType = "image/png";
        public const int FileSize = 5000000;

        public const string ClaimId = "id";
        public const string ClaimMail = "email";
        public const string ClaimFirstName = "first_name";
        public const string ClaimLastName = "last_name";
        public const string ClaimRoleName = "role_name";

        public const string InternalServerErrorMessage = "Internal Server Error";
        public const string InvalidCredentialsErrorMessage = "Invalid Credentials";

        public const string EventCreatedByGuest = "Guest";

        public const string PathForRefreshToken = "refresh-access/";
        public const string PathForEvents = "events/";
        public const string PathForUsers = "users/";

        private const string DefaultEventBannerFileName = "cardBanner.jpg";
        private const string DefaultProfileImageFileName = "defaultProfileImage.jpg";

        public const string DefaultEventBanner = $"{PathForEvents}{DefaultEventBannerFileName}";
        public const string DefaultProfileImage = $"{PathForUsers}{DefaultProfileImageFileName}";
    }

    public static class Regexes
    {
        public const string NamesExp = "^[a-zA-Z]+( [a-zA-Z]+)*$";
        public const string PasswordExp = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[\\W|_]).{8,32}$";
    }
}
