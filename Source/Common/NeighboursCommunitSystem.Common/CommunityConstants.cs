namespace NeighboursCommunitySystem.Common
{
    public static class CommunityConstants
    {
        public const int CommunityNameLengthMin = 3;
        public const int CommunityNameLengthMax = 30;
        public const string ShortNameErrorMessage = "Community name length should be equal to or more than 3 characters.";
        public const string LongNameErrorMessage = "Community name length should be equal to or shorter than 30 characters.";
        public const string NewCommunityAuthorizationMessage = "Only administrators can create new communities.";

        public const int DescriptionLengthMin = 3;
        public const int DescriptionLengthMax = 300;
        public const string ShortDescriptionErrorMessage = "Community description should be at least 3 characters long.";
        public const string LongDescriptionErrorMessage = "Community description should be no more than 300 characters long.";

        // Invitations Service
        public const string RegistrationInvitationMessage = "You can register for the Neighbours Community Management System on the following link --> {1} {0}Use this verification token in order to authorize your credentials --> {2}";
        public const string MailgunAuthenticationApi = "api";
        public const string MailgunAuthenticationKey = "key-1c6386f513a843fd177faf43651e104d";

        // Invitations Model
        public const int CommunityKeyMaxLength = 10;
        public const int CommunityKeyMinLength = 7;
        public const int EmailMaxLength = 150;
        public const int EmailMinLength = 3;

        public const string EmailValidationErrorMessage = "Invalid email address";
        public const string EmailValidationLengthErrorMessage = "Email length cannot exceed 150 characters.";
        public const string CommunityKeyMaxLengthErrorMessage = "Community key length cannot be more than 10 characters.";
        public const string CommunityKeyMinLengthErrorMessage = "Community key length cannot be less than 7 characters.";
    }
}