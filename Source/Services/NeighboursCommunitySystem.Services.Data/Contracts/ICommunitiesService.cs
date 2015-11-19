namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;
    using Server.DataTransferModels.Communities;

    public interface ICommunitiesService : IService
    {
        IQueryable<Community> All();

        Community GetById(int id);

        Community FindByName(string name);

        int Add(CommunityDataTransferModel model);

        int Update(CommunityDataTransferModel model);

        bool Remove(CommunityDataTransferModel model);

        bool RemoveById(int id);

        bool HasUser(int communityId, string userId);
    }
}
