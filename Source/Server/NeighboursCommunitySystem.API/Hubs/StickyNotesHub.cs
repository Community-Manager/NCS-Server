namespace NeighboursCommunitySystem.API.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("stickyNotesHub")]
    public class StickyNotesHub : Hub
    {
        public void AddProposal()
        {
            // It is calling a js function :)
            // Will explain later
            Clients.All.AddMe();
        }
    }
}