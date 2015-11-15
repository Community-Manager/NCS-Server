namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Web.Http;
    using Breeze.ContextProvider.EF6;
    using Breeze.WebApi2;
    using Data.DbContexts;
    using Services.Data.Contracts;

    [BreezeController]
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
        [EnableBreezeQuery]
        public IHttpActionResult Get()
        {
            var model = this.proposalService.All();
            return this.Ok(model);
        }

        [HttpPost]
        public IHttpActionResult VoteUp(int id)
        {
            this.proposalService.VoteUp(id);
            return this.Ok(id);
        }

        [HttpPost]
        public IHttpActionResult VoteDown(int id)
        {
            this.proposalService.VoteDown(id);
            return this.Ok(id);
        }
    }
}