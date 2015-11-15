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
            // It is calling a js function :)
            // Will explain later
            Clients.All.addMe();
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}