using System.Linq;
using System.Web.Http;
using Breeze.WebApi2;

namespace NeighboursCommunitySystem.API.Controllers
{
    using Breeze.ContextProvider.EF6;
    using Data.DbContexts;
    using Models;
    using Services.Data.Contracts;

    [BreezeController]
    public class BreezeController : ApiController
    {
        // Todo: inject via an interface rather than "new" the concrete class
        private readonly ICommunitiesService communities;
        private readonly IProposalService proposals;
        private readonly IInvitationService invitations;
        private readonly ITaxesService taxes;

        private readonly EFContextProvider<NeighboursCommunityDbContext> contextProvider =
            new EFContextProvider<NeighboursCommunityDbContext>();

        public BreezeController(ICommunitiesService communities, IProposalService proposals,
            IInvitationService invitations, ITaxesService taxes)
        {
            this.communities = communities;
            this.proposals = proposals;
            this.invitations = invitations;
            this.taxes = taxes;
        }

        [HttpGet]
        public string Metadata()
        {
            return contextProvider.Metadata();
        }

        //[HttpPost]
        //public SaveResult SaveChanges(JObject saveBundle)
        //{
        //    return _repository.SaveChanges(saveBundle);
        //}

        [HttpGet]
        public IQueryable<Community> Communities()
        {
            return this.communities.All();
        }

        [HttpGet]
        public IQueryable<Proposal> Proposals()
        {
            return this.proposals.All();
        }

        [HttpGet]
        public IQueryable<Tax> Taxes(int id)
        {
            return this.taxes.GetByCommunityId(id);
        }


        ///// <summary>
        ///// Query returing a 1-element array with a lookups object whose 
        ///// properties are all Rooms, Tracks, and TimeSlots.
        ///// </summary>
        ///// <returns>
        ///// Returns one object, not an IQueryable, 
        ///// whose properties are "rooms", "tracks", "timeslots".
        ///// The items arrive as arrays.
        ///// </returns>
        //[HttpGet]
        //public object Lookups()
        //{
        //    var rooms = _repository.Rooms;
        //    var tracks = _repository.Tracks;
        //    var timeslots = _repository.TimeSlots;
        //    return new { rooms, tracks, timeslots };
        //}

        // Diagnostic
        [HttpGet]
        public string Ping()
        {
            return "pong";
        }
    }
}
