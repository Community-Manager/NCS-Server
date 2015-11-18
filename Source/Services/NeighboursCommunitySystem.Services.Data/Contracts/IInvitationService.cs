namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System.Linq;
    using Models;
    using Server.DataTransferModels.Accounts;
    using System.Net;
    using System.Threading.Tasks;

    public interface IInvitationService : IService
    {
        IQueryable<Invitation> All();
        IQueryable<Invitation> GetByEmail(string email);

        int Add(Invitation invitationData);

        int Remove(string email);

        Task<HttpStatusCode> SendInvitation(AccountInvitationDataTransferModel invitationModel);
    }
}