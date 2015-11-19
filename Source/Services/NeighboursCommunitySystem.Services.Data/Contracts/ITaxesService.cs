namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using NeighboursCommunitySystem.Models;
    using Server.DataTransferModels.Taxes;

    public interface ITaxesService : IService
    {
        IQueryable<Tax> All();

        IQueryable<Tax> GetByCommunityId(int id);

        int Add(TaxRequestTransferModel model);

        Tax GetById(int id);

        void DeleteById(int id);

        void RemoveById(int id);

        void UpdateById(int id, TaxDataTransferModel model);
    }
}
