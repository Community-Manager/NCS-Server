namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using DtoModels.Taxes;
    using NeighboursCommunitySystem.Models;

    public interface ITaxesService : IService
    {
        IQueryable<Tax> All();

        IQueryable<Tax> GetByCommunityId(int id);

        int Add(TaxRequestTransferModel model);

        Tax GetById(int id);

        void DeleteById(int id);
    }
}
