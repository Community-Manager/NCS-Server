namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface ICommunitiesService : IService
    {
        IQueryable<Community> All();

        IQueryable<Community> ByCurrentUser();

        Community GetById(int id);

        int Add(string Name, string Description = null);
    }
}
