namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface ICommunitiesService : IService
    {
        IQueryable<Community> All();

        IQueryable<Community> ByCurrentUser();

        Community GetById(int id);

        bool HasUser(int communityId, string userId);

        int Add(string name, string description = null);
    }
}
