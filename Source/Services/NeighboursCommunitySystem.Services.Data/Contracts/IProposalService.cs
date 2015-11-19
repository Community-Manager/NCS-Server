﻿namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IProposalService : IService
    {
        IQueryable<Proposal> All();

        IQueryable<Proposal> GetByCommunity(int id);

        void VoteUp(int id, string userId);

        void VoteDown(int id, string userId);

        void VoteNeutral(int id, string userId);

        void Add(Proposal proposal, string userId, int communityId);
    }
}
