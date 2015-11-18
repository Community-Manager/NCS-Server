namespace NeighboursCommunitySystem.API.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("stickyNotesHub")]
    public class StickyNotesHub : Hub
    {


        public void AddProposal()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<StickyNotesHub>();
            context.Clients.All.addMe();
            // It is calling a js function :)
            // Will explain later
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}