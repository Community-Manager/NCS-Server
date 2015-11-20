namespace NeighboursCommunitySystem.API.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("stickyNotesHub")]
    public class StickyNotesHub : Hub
    {
        public void VoteProposal()
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<StickyNotesHub>();
            this.Clients.Others.refresh();
        }

        public void AddProposal()
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<StickyNotesHub>();
            this.Clients.Others.refreshAndRedirect();
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}