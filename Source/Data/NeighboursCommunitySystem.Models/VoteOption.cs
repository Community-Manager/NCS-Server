namespace NeighboursCommunitySystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VoteOption
    {
        private ICollection<Vote> votes;

        public VoteOption()
        {
            this.votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        [Required]
        public Options Option { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }
    }
}
