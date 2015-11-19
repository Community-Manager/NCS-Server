namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using Contracts;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;

    public class ProposalService : IProposalService
    {
        private readonly IRepository<Proposal> proposals;
        private readonly IRepository<Community> communities;

        public ProposalService(IRepository<Proposal> proposals, IRepository<Community> communities)
        {
            this.proposals = proposals;
            this.communities = communities;
        }

        public IQueryable<Proposal> All()
        {
            return this.proposals.All();
        }

        public void VoteUp(int id, string userId)
        {
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                proposal.Votes.Add(new Vote()
                {
                    OptionId = 1,
                    UserId = userId,
                    ProposalId = id
                });
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();

        }

        public void VoteDown(int id, string userId)
        {
            var proposal = this.proposals.GetById(id);


            if (proposal != null)
            {
                var voted = proposal.Votes.FirstOrDefault(v => v.UserId == userId);

                if (voted == null)
                {
                    proposal.Votes.Add(new Vote()
                    {
                        OptionId = 2,
                        UserId = userId,
                        ProposalId = id
                    });
                }
                else
                {
                    voted.OptionId = 2;
                }
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();
        }


        public void Add(Proposal proposal, string userId, int communityId)
        {

            var community = this.communities.All().FirstOrDefault(c => c.Id == communityId);
            var proposalToAdd = new Proposal()
            {
                Community = community,
                Description = proposal.Description,
                Title = proposal.Title,
                AuthorId = userId

            };

            this.proposals.Add(proposalToAdd);
            this.proposals.SaveChanges();
        }


        public IQueryable<Proposal> GetByCommunity(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Vote> GetVotes(int id)
        {
            throw new System.NotImplementedException();
        }

        public void VoteNeutral(int id, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
