namespace NeighboursCommunitySystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vote
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int ProposalId { get; set; }

        public int OptionId { get; set; } 

        public virtual User User { get; set; }

        public virtual Proposal Proposal { get; set; }

        public virtual VoteOption Option { get; set; }
    }
}
