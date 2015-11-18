namespace NeighboursCommunitySystem.Server.DataTransferModels.Communities
{
    using System.ComponentModel.DataAnnotations;
    using NeighboursCommunitySystem.Common;

    public class CommunityNameUpdateDataTransferModel
    {
        [Required]
        [MinLength(CommunityConstants.CommunityNameLengthMin, ErrorMessage = CommunityConstants.ShortNameErrorMessage)]
        [MaxLength(CommunityConstants.CommunityNameLengthMax, ErrorMessage = CommunityConstants.LongNameErrorMessage)]
        public string CurrentCommunityName { get; set; }

        [Required]
        [MinLength(CommunityConstants.CommunityNameLengthMin, ErrorMessage = CommunityConstants.ShortNameErrorMessage)]
        [MaxLength(CommunityConstants.CommunityNameLengthMax, ErrorMessage = CommunityConstants.LongNameErrorMessage)]
        public string NewCommunityName { get; set; }
    }
}