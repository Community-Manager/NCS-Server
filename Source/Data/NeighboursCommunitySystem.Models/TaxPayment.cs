namespace NeighboursCommunitySystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TaxPayment
    {
        [Key, Column(Order = 0)]
        public int TaxId { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }

        public decimal AmountPaid { get; set; }

        public virtual Tax Tax { get; set; }

        public virtual User User { get; set; }
    }
}
