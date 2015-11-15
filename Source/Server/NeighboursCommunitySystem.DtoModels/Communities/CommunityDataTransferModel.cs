namespace NeighboursCommunitySystem.DtoModels.Communities
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class CommunityDataTransferModel
    {
        [Required]
        [MinLength(CommunityConstants.CommunityNameLengthMin, ErrorMessage = CommunityConstants.ShortNameErrorMessage)]
        [MaxLength(CommunityConstants.CommunityNameLengthMax, ErrorMessage = CommunityConstants.LongNameErrorMessage)]
        public string Name { get; set; }

        [MinLength(CommunityConstants.DescriptionLengthMin, ErrorMessage = CommunityConstants.ShortDescriptionErrorMessage)]
        [MaxLength(CommunityConstants.DescriptionLengthMax, ErrorMessage = CommunityConstants.LongDescriptionErrorMessage)]
        public string Description { get; set; }
    }
}
