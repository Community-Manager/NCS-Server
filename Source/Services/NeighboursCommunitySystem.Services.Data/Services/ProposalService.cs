namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using Contracts;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;

    public class ProposalService : IProposalService
    {
        private readonly IRepository<Proposal> proposals;

        public ProposalService(IRepository<Proposal> proposals)
        {
            this.proposals = proposals;
        }

        public IQueryable<Proposal> All()
        {
            return this.proposals.All();
        }




        public void VoteUp(int id)
        {
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                proposal.Votes.Add(new Vote()
                {
                    OptionId = 1,
                    UserId = "1"
                });
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();

        }

        public void VoteDown(int id)
        {
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                proposal.Votes.Add(new Vote()
                {
                    OptionId = 2,
                    UserId = "2"
                });
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();
        }
    }
}
