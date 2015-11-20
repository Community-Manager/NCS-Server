namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using AutoMapper.QueryableExtensions;
    using Breeze.ContextProvider.EF6;
    using Breeze.WebApi2;
    using Data.DbContexts;
    using Microsoft.AspNet.Identity;
    using Models;
    using Server.DataTransferModels.Proposals;
    using Services.Data.Contracts;

    [BreezeController]
    //[EnableCors(origins: "http://neighbourscommunityclient.azurewebsites.net, http://localhost:53074", headers: "*", methods: "*")]
    public class ProposalsController : ApiController
    {
        private readonly IProposalService proposalService;
        private readonly ICommunitiesService communityService;
        private readonly IMappingService mappingService;

        private readonly EFContextProvider<NeighboursCommunityDbContext> contextProvider =
            new EFContextProvider<NeighboursCommunityDbContext>();

        public ProposalsController(IProposalService proposalService, IMappingService mappingService, ICommunitiesService communityService)
        {
            this.proposalService = proposalService;
            this.mappingService = mappingService;
            this.communityService = communityService;
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

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetByCommunity(int id)
        {
            var porposalsByCommunity = this.proposalService
                                            .GetByCommunity(id)
                                            .ProjectTo<ProposalResponseModel>()
                                            .ToList();

            return this.Ok(porposalsByCommunity);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Votes(int id)
        {
            var votes = this.proposalService
                            .GetVotes(id)
                            .ProjectTo<VoteResponseModel>()
                            .ToList();

            return this.Ok(votes);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult VoteUp(int id)
        {

            var userId = this.User.Identity.GetUserId();
            var action = this.proposalService.VoteUp(id, userId);
            return this.Ok(action);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult VoteDown(int id)
        {
            var userId = this.User.Identity.GetUserId();
            var action = this.proposalService.VoteDown(id, userId);
            return this.Ok(action);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult VoteNeutral(int id)
        {
            var userId = this.User.Identity.GetUserId();
            this.proposalService.VoteNeutral(id, userId);
            return this.Ok(id);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(ProposalDataTransferModel proposalModel)
        {
            var community = this.communityService.FindByName(proposalModel.CommunityName);
            var communityId = community.Id;
            var userId = this.User.Identity.GetUserId();
            this.proposalService.Add(this.mappingService.Map<Proposal>(proposalModel), userId, communityId);
            return this.Ok();
        }
    }
}