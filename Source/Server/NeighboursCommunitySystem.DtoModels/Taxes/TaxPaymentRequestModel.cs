namespace NeighboursCommunitySystem.Server.DataTransferModels.Taxes
{
    using System.ComponentModel.DataAnnotations;

    public class TaxPaymentRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
