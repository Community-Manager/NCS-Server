namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Services.Data.Contracts;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using Microsoft.AspNet.Identity;
    using Models;
    using Server.DataTransferModels.Taxes;
    using Server.Infrastructure.Validation;

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

        [Authorize]
        public IHttpActionResult Get()
        {
            var allTaxes = taxes.All().ProjectTo<TaxDataTransferModel>().ToList();
            
            return this.Ok(allTaxes);
        }

        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Get(int id)
        {
            var tax = taxes.GetById(id);
            var taxResponse = Mapper.Map<TaxDataTransferModel>(tax);

            if (!ValidateCurrentUserCommunity(communities.GetById(tax.CommunityId)))
            {
                return this.Unauthorized();
            }

            return this.Ok(taxResponse);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Accountant")]
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

        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Post(TaxRequestTransferModel model)
        {
            //if (!ValidateCurrentUserCommunity(communities.GetById(model.CommunityId)))
            //{
            //    return this.Unauthorized();
            //}

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            int taxId = taxes.Add(model);

            return this.Ok(taxId);
        }

        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Delete(int id)
        {
            if (!ValidateCurrentUserCommunity(communities.GetById(taxes.GetById(id).CommunityId)))
            {
                return this.Unauthorized();
            }

            taxes.DeleteById(id);

            return this.Ok();
        }

        [Authorize(Roles = "Administrator,Accountant")]
        [ValidateModelAttribute]
        public IHttpActionResult Put(int id, TaxDataTransferModel model)
        {
            if (!ValidateCurrentUserCommunity(communities.GetById(taxes.GetById(id).CommunityId)))
            {
                return this.Unauthorized();
            }

            taxes.UpdateById(id, model);

            return this.Ok();
        }

        // Action Filter override Execute
        [HttpGet]
        [Authorize(Roles = "Administrator,Accountant")]
        [ValidateModelAttribute]
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
        [Authorize(Roles = "Administrator,Accountant")]
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

        // Extract to Validation Folder in a Validator class.
        [NonAction]
        public bool ValidateCurrentUserCommunity(Community currentCommunity)
        {
            return currentCommunity.Users.Any(u => u.Id == currentUserId);
        }
    }
}