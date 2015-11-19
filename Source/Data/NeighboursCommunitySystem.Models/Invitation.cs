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
        [Index(IsUnique = true)]
        [EmailAddress(ErrorMessage = CommunityConstants.EmailValidationErrorMessage)]
        [MaxLength(CommunityConstants.EmailMaxLength, ErrorMessage = CommunityConstants.EmailValidationMaxLengthErrorMessage)]
        [MinLength(CommunityConstants.EmailMinLength, ErrorMessage = CommunityConstants.EmailValidationMinLengthErrorMessage)]
        public string Email { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(51, ErrorMessage = "Invalid verification token - length must not be greater than 51 characters.")]
        [MinLength(46, ErrorMessage = "Invalid verification token - length must not be less than 47 characters.")]
        public string VerificationToken { get; set; }
    }
}