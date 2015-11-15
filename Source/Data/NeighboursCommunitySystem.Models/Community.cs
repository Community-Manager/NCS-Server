namespace NeighboursCommunitySystem.Models
{
    using Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Community
    {
        private ICollection<User> users;
        private ICollection<Tax> taxes;
        private ICollection<Proposal> proposals;

        public Community()
        {
            this.users = new HashSet<User>();
            this.taxes = new HashSet<Tax>();
            this.proposals = new HashSet<Proposal>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(CommunityConstants.CommunityNameLengthMin)]
        [MaxLength(CommunityConstants.CommunityNameLengthMax)]
        public string Name { get; set; }

        [MinLength(CommunityConstants.DescriptionLengthMin)]
        [MaxLength(CommunityConstants.DescriptionLengthMax)]
        public string Description { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<Tax> Taxes
        {
            get { return this.taxes; }
            set { this.taxes = value; }
        }

        public virtual ICollection<Proposal> Proposals
        {
            get { return this.proposals; }
            set { this.proposals = value; }
        }
    }
}