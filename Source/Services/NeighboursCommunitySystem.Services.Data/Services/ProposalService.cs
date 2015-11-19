﻿namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using Contracts;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;

    public class ProposalService : IProposalService
    {
        private readonly IRepository<Proposal> proposals;
        private readonly IRepository<Community> communities;

        public ProposalService(IRepository<Proposal> proposals)
        {
            this.proposals = proposals;
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
                proposal.Votes.Add(new Vote()
                {
                    OptionId = 2,
                    UserId = "2"
                });
            }

            this.proposals.Update(proposal);
            this.proposals.SaveChanges();
        }


        public void Add(Proposal proposal)
        {
            var community = this.communities.All().FirstOrDefault(c => c.Name == proposal.Community.Name);
            var proposalToAdd = new Proposal()
            {
                Community = community,
                Description = proposal.Description,
                Title = proposal.Title,
                AuthorId = proposal.AuthorId

            };

            this.proposals.Add(proposalToAdd);
            this.proposals.SaveChanges();
        }
    }
}
