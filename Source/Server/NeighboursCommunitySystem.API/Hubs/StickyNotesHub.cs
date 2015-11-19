namespace NeighboursCommunitySystem.API.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("stickyNotesHub")]
    public class StickyNotesHub : Hub
    {
        public void VoteUpProposal()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<StickyNotesHub>();
            context.Clients.All.refresh();
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}