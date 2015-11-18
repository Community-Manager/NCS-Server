namespace NeighboursCommunitySystem.Server.DataTransferModels.Communities
{
    using NeighboursCommunitySystem.Common;
    using Accounts;
    using System.ComponentModel.DataAnnotations;

    public class CommunityWithAdminDataTransferModel
    {
        // Administator Data
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(40, ErrorMessage = "First Name can have a maximum length of 40 characters and minimum length of {2} characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(40, ErrorMessage = "Last Name can have a maximum length of 40 characters and minimum length of {2} characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Appartament Number")]
        public byte ApartmentNumber { get; set; }

        // Community Data
        public CommunityDataTransferModel CommunityModel { get; set; }
    }
}