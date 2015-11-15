namespace NeighboursCommunitySystem.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        private ICollection<Vote> votes;
        private ICollection<Proposal> proposals;
        private ICollection<TaxPayment> payments;
        private ICollection<Community> communities;

        public User()
        {
            this.votes = new HashSet<Vote>();
            this.proposals = new HashSet<Proposal>();
            this.payments = new HashSet<TaxPayment>();
            this.communities = new HashSet<Community>();
        }

        public User(string userName)
            : base(userName)
        {
            this.votes = new HashSet<Vote>();
            this.proposals = new HashSet<Proposal>();
            this.payments = new HashSet<TaxPayment>();
            this.communities = new HashSet<Community>();
        }

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public byte ApartmentNumber { get; set; }
        
        public virtual ICollection<Proposal> Proposals
        {
            get { return this.proposals; }
            set { this.proposals = value; }
        }

        public virtual ICollection<Community> Communities
        {
            get { return this.communities; }
            set { this.communities = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public virtual ICollection<TaxPayment> Payments
        {
            get { return this.payments; }
            set { this.payments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}