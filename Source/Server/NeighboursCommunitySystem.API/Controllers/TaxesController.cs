namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Services.Data.Contracts;
    using DtoModels.Taxes;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;

    public class TaxesController : ApiController
    {
        private readonly ITaxesService taxes;

        public TaxesController(ITaxesService taxes)
        {
            this.taxes = taxes;
        }

        [Authorize(Roles = "DbAdmin")]
        public IHttpActionResult Get()
        {
            var allTaxes = taxes.All().ProjectTo<TaxDataTransferModel>().ToList();

            return this.Ok(allTaxes);
        }

        [Authorize(Roles = "DbAdmin")]
        public IHttpActionResult Get(int id)
        {
            var tax = Mapper.Map<TaxDataTransferModel>(taxes.GetById(id));

            return this.Ok(tax);
        }

        [HttpGet]
        [Authorize(Roles = "DbAdmin,Administrator,Accountant")]
        public IHttpActionResult Community(int id)
        {
            var communityTaxes = taxes
                                .GetByCommunityId(id)
                                .ProjectTo<TaxDataTransferModel>()
                                .ToList();

            return this.Ok(communityTaxes);
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Post(TaxRequestTransferModel model)
        {
            int taxId = taxes.Add(model);

            return this.Ok(taxId);
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Delete(int id)
        {
            taxes.DeleteById(id);

            return this.Ok();
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Put(int id, TaxDataTransferModel model)
        {
            taxes.UpdateById(id, model);

            return this.Ok();
        }

        [HttpGet]
        [Authorize(Roles = "DbAdmin,Administrator,Accountant")]
        public IHttpActionResult Available(int id)
        {
            var availableTaxes = taxes
                                    .GetByCommunityId(id)
                                    .Where(t => t.Deadline > DateTime.Now)
                                    .ProjectTo<TaxDataTransferModel>()
                                    .ToList();

            return this.Ok(availableTaxes);
        }

        [HttpGet]
        [Authorize(Roles = "DbAdmin,Administrator,Accountant")]
        public IHttpActionResult Expired(int id)
        {
            var expiredTaxes = taxes
                                    .GetByCommunityId(id)
                                    .Where(t => t.Deadline < DateTime.Now)
                                    .ProjectTo<TaxDataTransferModel>()
                                    .ToList();

            return this.Ok(expiredTaxes);
        }
    }
}