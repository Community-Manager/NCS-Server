namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using System.Collections.Generic;
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

        public void UpdateById(int id, TaxDataTransferModel model)
        {
            var tax = this.GetById(id);

            tax.Name = model.Name;
            tax.Price = model.Price;
            tax.Description = model.Description;
            tax.Deadline = model.Deadline;

            taxes.SaveChanges();
        }

        public Tax GetById(int id)
        {
            var tax = taxes.GetById(id);

            return tax;
        }

        public IQueryable<Tax> GetByCommunityId(int id)
        {
            return taxes.All().Where(t => t.Community.Id == id);
        }

        public int Add(TaxRequestTransferModel model)
        {
            var tax = new Tax
            {
                Name = model.Name,
                Price = model.Price,
                Deadline = model.Deadline,
                Description = model.Description,
                CommunityId = model.CommunityId
            };

            taxes.Add(tax);
            taxes.SaveChanges();

            return tax.Id;
        }
    }
}
