namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using Contracts;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;
    using Server.DataTransferModels.Taxes;

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
            var tax = taxes.GetById(id);
            tax.IsDeleted = true;

            taxes.SaveChanges();
        }

        public void RemoveById(int id)
        {
            taxes.Delete(id);
            taxes.SaveChanges();
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
            return taxes.GetById(id);
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

        public void AddPayment(int taxId, string userId, decimal amount)
        {
            var tax = taxes.GetById(taxId);
            var payment = tax.Payments.FirstOrDefault(p => p.TaxId == taxId && p.UserId == userId);

            if (payment != null)
            {
                payment.AmountPaid = amount;
            }
            else
            {
                tax.Payments.Add(new TaxPayment
                {
                    TaxId = taxId,
                    UserId = userId,
                    AmountPaid = amount
                });
            }
            
            taxes.SaveChanges();
        }
    }
}
