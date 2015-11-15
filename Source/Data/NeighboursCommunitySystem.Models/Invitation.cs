namespace NeighboursCommunitySystem.Models
{
    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;

    public class Invitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = CommunityConstants.EmailValidationLengthErrorMessage)]
        [EmailAddress(ErrorMessage = CommunityConstants.EmailValidationErrorMessage)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public byte[] VerificationToken { get; set; }

        [Required]
        [MaxLength(1000)]
        public byte[] DecryptionKey { get; set; }

        [Required]
        [MaxLength(1000)]
        public byte[] InitializationVector { get; set; }
    }
}