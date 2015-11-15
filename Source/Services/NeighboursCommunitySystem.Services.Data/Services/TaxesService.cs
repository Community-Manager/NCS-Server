namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using Contracts;
    using DtoModels.Taxes;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;

    public class TaxesService : ITaxesService
    {
        private readonly IRepository<Tax> taxes;

        public TaxesService(IRepository<Tax> taxes)
        {
            this.taxes = taxes;
        }

        public IQueryable<Tax> All()
        {
            return taxes.All();
        }

        public void DeleteById(int id)
        {
            taxes.Delete(taxes.GetById(id));
        }

        public IQueryable<Tax> GetByCommunityId(int Id)
        {
            return taxes.All().Where(t => t.Community.Id == Id);
        }

        public int AddByCommunityId(int communityId, TaxDataTransferModel model)
        {
            var tax = new Tax
            {
                Name = model.Name,
                Price = model.Price,
                Deadline = model.Deadline,
                Description = model.Description,
                CommunityId = communityId
            };

            taxes.Add(tax);
            taxes.SaveChanges();

            return tax.Id;
        }
    }
}
