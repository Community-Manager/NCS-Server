namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IProposalService : IService
    {
        IQueryable<Proposal> All();

        void VoteUp(int id, string userId);

        void VoteDown(int id, string userId);

        void Add(Proposal proposal, string userId, int communityId);


    }
}
