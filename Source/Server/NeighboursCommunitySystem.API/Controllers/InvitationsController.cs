namespace NeighboursCommunitySystem.API.Controllers
{
    using Models;
    using DtoModels.Accounts;
    using Services.Data.Contracts;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class InvitationsController : ApiController
    {
        private readonly IInvitationService invitationService;

        public InvitationsController(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        [HttpGet]
        [ResponseType(typeof(IQueryable<Invitation>))]
        public async Task<IHttpActionResult> Get()
        {
            var invitations = await this.invitationService.All().ToListAsync();
            return this.Ok(invitations);
        }

        // Route "api/invitations", METHOD(POST), ContentType:application/json, Body { "email":"email@domain.com", "communityKey" : "BGSFSL164" }
        [HttpPost]
        public IHttpActionResult SendInvitation(AccountInvitationDataTransferModel invitationModel)
        {
            if(!ModelState.IsValid)
            {
                return this.StatusCode(HttpStatusCode.NotAcceptable);
            }

            var response = this.invitationService.SendInvitation(invitationModel);
            return this.Ok(response);
        }
    }
}