namespace NeighboursCommunitySystem.Server.DataTransferModels.Accounts
{
    using NeighboursCommunitySystem.Common;
    using System.ComponentModel.DataAnnotations;

    public class AccountInvitationDataTransferModel
    {
        [Required]
        [EmailAddress(ErrorMessage = CommunityConstants.EmailValidationErrorMessage)]
        [MaxLength(CommunityConstants.EmailMaxLength, ErrorMessage = CommunityConstants.EmailValidationMaxLengthErrorMessage)]
        [MinLength(CommunityConstants.EmailMinLength, ErrorMessage = CommunityConstants.EmailValidationMinLengthErrorMessage)]
        public string Email { get; set; }

        [Required]
        [MaxLength(CommunityConstants.CommunityKeyMaxLength, ErrorMessage = CommunityConstants.CommunityKeyMaxLengthErrorMessage)]
        [MinLength(CommunityConstants.CommunityKeyMinLength, ErrorMessage = CommunityConstants.CommunityKeyMinLengthErrorMessage)]
        public string CommunityKey { get; set; }
    }
}