namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Web.Http;
    using Breeze.ContextProvider.EF6;
    using Breeze.WebApi2;
    using Data.DbContexts;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [BreezeController]

    //[EnableCors(origins: "http://neighbourscommunityclient.azurewebsites.net, http://localhost:53074", headers: "*", methods: "*")]
    public class ProposalsController : ApiController
    {
        private readonly IProposalService proposalService;

        private readonly EFContextProvider<NeighboursCommunityDbContext> contextProvider =
            new EFContextProvider<NeighboursCommunityDbContext>();

        public ProposalsController(IProposalService proposalService)
        {
            this.proposalService = proposalService;
        }

        [HttpGet]
        public string Metadata()
        {
            return contextProvider.Metadata();
        }

        [HttpGet]
        [Authorize]
        [EnableBreezeQuery]
        public IHttpActionResult Get()
        {
            var model = this.proposalService.All();
            return this.Ok(model);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult VoteUp(int id)
        {
            var userId = this.User.Identity.GetUserId();
            this.proposalService.VoteUp(id, userId);
            return this.Ok(id);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult VoteDown(int id)
        {
            var userId = this.User.Identity.GetUserId();
            this.proposalService.VoteDown(id, userId);
            return this.Ok(id);
        }
    }
}