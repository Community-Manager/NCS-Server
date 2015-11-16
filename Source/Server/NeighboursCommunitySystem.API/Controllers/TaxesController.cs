namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Services.Data.Contracts;
    using DtoModels.Taxes;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using Microsoft.AspNet.Identity;
    using Models;
    using Data.Repositories;
    using Data.DbContexts;

    public class TaxesController : ApiController
    {
        private readonly ITaxesService taxes;
        private readonly ICommunitiesService communities;
        private readonly string currentUserId;

        public TaxesController(ITaxesService taxes, ICommunitiesService communities)
        {
            this.taxes = taxes;
            this.communities = communities;
            this.currentUserId = this.User.Identity.GetUserId();
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
            if (!ValidateCurrentUserCommunity(communities.GetById(id)))
            {
                return this.Unauthorized();
            }

            var communityTaxes = taxes
                                    .GetByCommunityId(id)
                                    .ProjectTo<TaxDataTransferModel>()
                                    .ToList();

            return this.Ok(communityTaxes);
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Post(TaxRequestTransferModel model)
        {
            if (!ValidateCurrentUserCommunity(communities.GetById(model.CommunityId)))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            int taxId = taxes.Add(model);

            return this.Ok(taxId);
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Delete(int id)
        {
            if (!ValidateCurrentUserCommunity(communities.GetById(taxes.GetById(id).CommunityId)))
            {
                return this.Unauthorized();
            }

            taxes.DeleteById(id);

            return this.Ok();
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Put(int id, TaxDataTransferModel model)
        {
            if (!ValidateCurrentUserCommunity(communities.GetById(taxes.GetById(id).CommunityId)))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            taxes.UpdateById(id, model);

            return this.Ok();
        }

        [HttpGet]
        [Authorize(Roles = "DbAdmin,Administrator,Accountant")]
        public IHttpActionResult Available(int id)
        {
            if (!ValidateCurrentUserCommunity(communities.GetById(id)))
            {
                return this.Unauthorized();
            }

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
            if (!ValidateCurrentUserCommunity(communities.GetById(id)))
            {
                return this.Unauthorized();
            }

            var expiredTaxes = taxes
                                .GetByCommunityId(id)
                                .Where(t => t.Deadline < DateTime.Now)
                                .ProjectTo<TaxDataTransferModel>()
                                .ToList();

            return this.Ok(expiredTaxes);
        }

        [NonAction]
        public bool ValidateCurrentUserCommunity(Community currentCommunity)
        {
            return currentCommunity.Users.Any(u => u.Id == currentUserId);
        }
    }
}