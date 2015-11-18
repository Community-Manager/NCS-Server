namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;
    using Server.DataTransferModels.Accounts;
    using System.Net;

    public interface IInvitationService : IService
    {
        IQueryable<Invitation> All();
        IQueryable<Invitation> GetBy
            (string email);

        int Add(Invitation invitationData);

        int Remove(string email);

        HttpStatusCode SendInvitation(AccountInvitationDataTransferModel invitationModel);
    }
}