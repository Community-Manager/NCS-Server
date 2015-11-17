namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;
    using Server.DataTransferModels.Accounts;

    public interface IInvitationService : IService
    {
        IQueryable<Invitation> All();
        IQueryable<Invitation> GetByEmail(string email);

        int Add(Invitation invitationData);

        int Remove(string email);

        string SendInvitation(AccountInvitationDataTransferModel invitationModel);
    }
}