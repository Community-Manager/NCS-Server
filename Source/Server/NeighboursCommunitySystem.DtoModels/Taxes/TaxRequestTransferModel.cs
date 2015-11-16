namespace NeighboursCommunitySystem.DtoModels.Taxes
{
    using System.ComponentModel.DataAnnotations;

    public class TaxRequestTransferModel : TaxDataTransferModel
    {
        [Required]
        public int CommunityId { get; set; }
    }
}
