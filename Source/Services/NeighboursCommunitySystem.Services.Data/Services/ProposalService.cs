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

        public IQueryable<Proposal> GetByCommunity(int id)
        {
            return this.proposals.All().Where(p => p.CommunityId == id);
        }

        public void VoteUp(int id, string userId)
        {
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                var vote = proposal.Votes.FirstOrDefault(p => p.ProposalId == id && p.UserId == userId);

                if (vote != null)
                {
                    vote.OptionId = 1;
                }
                else
                {
                    proposal.Votes.Add(new Vote()
                    {
                        OptionId = 1,
                        UserId = userId,
                        ProposalId = id
                    });
                }
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();

        }

        public void VoteDown(int id, string userId)
        {
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                var vote = proposal.Votes.FirstOrDefault(p => p.ProposalId == id && p.UserId == userId);

                if (vote != null)
                {
                    vote.OptionId = 2;
                }
                else
                {
                    proposal.Votes.Add(new Vote()
                    {
                        OptionId = 2,
                        UserId = userId,
                        ProposalId = id
                    });
                }
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();
        }

        public void VoteNeutral(int id, string userId)
        {
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                var vote = proposal.Votes.FirstOrDefault(p => p.ProposalId == id && p.UserId == userId);

                if (vote != null)
                {
                    vote.OptionId = 3;
                }
                else
                {
                    proposal.Votes.Add(new Vote()
                    {
                        OptionId = 3,
                        UserId = userId,
                        ProposalId = id
                    });
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
    }
}
