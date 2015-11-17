namespace NeighboursCommunitySystem.Server.DataTransferModels.Accounts
{
    using NeighboursCommunitySystem.Common;
    using System.ComponentModel.DataAnnotations;

    public class AccountInvitationDataTransferModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(CommunityConstants.EmailMaxLength, ErrorMessage = CommunityConstants.EmailValidationLengthErrorMessage)]
        [MinLength(CommunityConstants.EmailMinLength, ErrorMessage = CommunityConstants.EmailValidationErrorMessage)]
        public string Email { get; set; }

        [Required]
        [MaxLength(CommunityConstants.CommunityNameLengthMax, ErrorMessage = CommunityConstants.CommunityKeyMaxLengthErrorMessage)]
        [MinLength(CommunityConstants.CommunityNameLengthMin, ErrorMessage = CommunityConstants.CommunityKeyMinLengthErrorMessage)]
        public string CommunityKey { get; set; }
    }
}