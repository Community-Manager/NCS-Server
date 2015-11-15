namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IProposalService : IService
    {
        IQueryable<Proposal> All();
        void VoteUp(int id);
        void VoteDown(int id);

    }
}
