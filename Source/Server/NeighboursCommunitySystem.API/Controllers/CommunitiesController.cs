namespace NeighboursCommunitySystem.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Services.Data.Contracts;
    using DtoModels.Communities;
    using Models;
    using Common;

    public class CommunitiesController : ApiController
    {
        private readonly ICommunitiesService communities;

        public CommunitiesController(ICommunitiesService communities)
        {
            this.communities = communities;
        }

        public IHttpActionResult Get()
        {
            var result = communities
                .All()
                .Select(c => new CommunityDataTransferModel()
                {
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            return this.Ok(result);
        }

        [Authorize(Roles = "DbAdmin,Administrator")]
        public IHttpActionResult Post(CommunityDataTransferModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            
            if (communities.All().Any(c => c.Name == model.Name))
            {
                return this.BadRequest(GlobalConstants.UniqueNameErrorMessage);
            }

            var newCommunityId = communities.Add(model.Name, model.Description);

            return this.Ok(newCommunityId);
        }
    }
}