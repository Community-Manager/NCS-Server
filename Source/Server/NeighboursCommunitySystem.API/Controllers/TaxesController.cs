namespace NeighboursCommunitySystem.API.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Server.DataTransferModels.Taxes;
    using Server.Infrastructure.Validation;
    using ServerConstants = Server.Common.Constants.Constants;
    using Services.Data.Contracts;

    //[EnableCors(origins: "http://neighbourscommunityclient.azurewebsites.net, http://localhost:53074", headers: "*", methods: "*")]
    public class TaxesController : ApiController
    {
        private readonly ITaxesService taxes;
        private readonly ICommunitiesService communities;
        private string currentUserId;

        public TaxesController(ITaxesService taxes, ICommunitiesService communities)
        {
            this.taxes = taxes;
            this.communities = communities;
        }

        // There should be no action to get all taxes for all communities by a logged in user.
        //
        //[Authorize]
        //public IHttpActionResult Get()
        //{
        //    var allTaxes = taxes.All().ProjectTo<TaxDataTransferModel>().ToList();
        //    return this.Ok(allTaxes);
        //}

        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Get(int id)
        {
            var tax = taxes.GetById(id);

            if (tax == null)
            {
                return this.BadRequest(string.Format(ServerConstants.NoItemWithIdErrorMessageFormat, id));
            }

            var taxResponse = Mapper.Map<TaxDataTransferModel>(tax);

            this.currentUserId = this.User.Identity.GetUserId();

            if (!ValidateCurrentUserCommunity(tax.CommunityId))
            {
                return this.Unauthorized();
            }

            return this.Ok(taxResponse);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Community(int id)
        {
            this.currentUserId = this.User.Identity.GetUserId();

            if (!ValidateCurrentUserCommunity(id))
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
            this.currentUserId = this.User.Identity.GetUserId();

            if (!ValidateCurrentUserCommunity(model.CommunityId))
            {
                return this.Unauthorized();
            }

            int taxId = taxes.Add(model);

            return this.Ok(taxId);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Delete(int id)
        {
            this.currentUserId = this.User.Identity.GetUserId();

            var tax = taxes.GetById(id);

            if (tax == null)
            {
                return this.BadRequest(string.Format(ServerConstants.NoItemWithIdErrorMessageFormat, id));
            }

            var communityId = taxes.GetById(id).CommunityId; ;

            if (!ValidateCurrentUserCommunity(communityId))
            {
                return this.Unauthorized();
            }

            taxes.DeleteById(id);

            return this.Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Remove(int id)
        {
            this.currentUserId = this.User.Identity.GetUserId();

            var tax = taxes.GetById(id);

            if (tax == null)
            {
                return this.BadRequest(string.Format(ServerConstants.NoItemWithIdErrorMessageFormat, id));
            }

            var communityId = taxes.GetById(id).CommunityId; ;

            if (!ValidateCurrentUserCommunity(communityId))
            {
                return this.Unauthorized();
            }

            taxes.RemoveById(id);

            return this.Ok();
        }

        [HttpPut]
        [ValidateModel]
        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Update(int id, TaxDataTransferModel model)
        {
            this.currentUserId = this.User.Identity.GetUserId();

            var tax = taxes.GetById(id);

            if (tax == null)
            {
                return this.BadRequest(string.Format(ServerConstants.NoItemWithIdErrorMessageFormat, id));
            }

            if (!ValidateCurrentUserCommunity(tax.CommunityId))
            {
                return this.Unauthorized();
            }

            taxes.UpdateById(id, model);

            return this.Ok();
        }

        [HttpGet]
        [ValidateModel]
        [Authorize(Roles = "Administrator,Accountant")]
        public IHttpActionResult Available(int id)
        {
            this.currentUserId = this.User.Identity.GetUserId();

            if (!ValidateCurrentUserCommunity(id))
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
            this.currentUserId = this.User.Identity.GetUserId();

            if (!ValidateCurrentUserCommunity(id))
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
        public bool ValidateCurrentUserCommunity(int communityId)
        {
            return this.communities.HasUser(communityId, currentUserId);
        }
    }
}