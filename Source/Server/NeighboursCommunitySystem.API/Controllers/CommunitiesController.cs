namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Breeze.WebApi2;
    using Common;
    using Server.DataTransferModels.Communities;
    using Services.Data.Contracts;

    [BreezeController]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommunitiesController : ApiController
    {
        private readonly ICommunitiesService communities;

        public CommunitiesController(ICommunitiesService communities)
        {
            this.communities = communities;
        }

        [HttpGet]
        [EnableBreezeQuery]
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

        // api/
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