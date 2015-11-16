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
        [MaxLength(50, ErrorMessage = "Invalid verification token - length must not be greater than 50 characters.")]
        [MinLength(47, ErrorMessage = "Invalid verification token - length must not be less than 47 characters.")]
        public string VerificationToken { get; set; }
    }
}