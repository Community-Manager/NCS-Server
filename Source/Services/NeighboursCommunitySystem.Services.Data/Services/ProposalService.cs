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
        private readonly IRepository<Vote> votes;
        private readonly IRepository<User> users;

        public ProposalService(IRepository<Proposal> proposals, IRepository<Community> communities, IRepository<Vote> votes, IRepository<User> users)
        {
            this.proposals = proposals;
            this.communities = communities;
            this.votes = votes;
            this.users = users;
        }

        public IQueryable<Proposal> All()
        {
            return this.proposals.All();
        }

        public int VoteUp(int id, string userId)
        {
            var upOrDown = 0;
            var user = this.users.GetById(userId);
            var proposal = this.proposals.GetById(id);

            if (proposal != null)
            {
                var voted = proposal.Votes.FirstOrDefault(v => v.UserId == userId);

                if (voted == null)
                {
                    var vote = new Vote()
                    {
                        OptionId = 1,
                        User = user,
                        ProposalId = id
                    };

                    upOrDown = 1;
                    user.Votes.Add(vote);
                    this.users.SaveChanges();

                    this.votes.SaveChanges();

                }
                else
                {
                    if (voted.OptionId == 1)
                    {
                        voted.OptionId = 3;
                        upOrDown = 0;

                    }
                    else if (voted.OptionId == 2 || voted.OptionId == 3)
                    {
                        voted.OptionId = 1;
                        upOrDown = 1;

                    }

                    this.votes.SaveChanges();
                }

                this.proposals.Update(proposal);
                this.proposals.SaveChanges();
            }

            return upOrDown;
        }

        public int VoteDown(int id, string userId)
        {
            var user = this.users.GetById(userId);

            var proposal = this.proposals.GetById(id);
            var upOrDown = 0;

            if (proposal != null)
            {
                var voted = proposal.Votes.FirstOrDefault(v => v.UserId == userId);

                if (voted == null)
                {
                    var vote = new Vote()
                    {
                        OptionId = 2,
                        User = user,
                        ProposalId = id
                    };

                    upOrDown = 1;

                    user.Votes.Add(vote);
                    this.users.SaveChanges();

                    this.votes.SaveChanges();
                }
                else
                {
                    if (voted.OptionId == 2)
                    {
                        voted.OptionId = 3;
                        upOrDown = 0;
                    }
                    else if (voted.OptionId == 1 || voted.OptionId == 3)
                    {
                        voted.OptionId = 2;
                        upOrDown = 1;
                    }
                    this.votes.SaveChanges();
                }
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();
            return upOrDown;
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
