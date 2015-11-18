namespace NeighboursCommunitySystem.Services.Data.Services
{
    using Contracts;
    using System;
    using System.Security.Cryptography;
    using System.Linq;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;
    using RestSharp;
    using RestSharp.Authenticators;
    using Server.Common.Generators;
    using Server.Common.Constants;
    using System.IO;
    using System.Net;
    using Common;
    using Server.DataTransferModels.Accounts;

    public class InvitationService : IInvitationService
    {
        private readonly IRepository<Invitation> invitations;
        private readonly IRepository<Community> communities;

        public InvitationService(IRepository<Invitation> invitations, IRepository<Community> communities)
        {
            this.invitations = invitations;
            this.communities = communities;
        }

        public IQueryable<Invitation> All()
        {
            return this.invitations.All().AsQueryable();
        }

        public IQueryable<Invitation> GetByEmail(string email)
        {
            // TODO: Implement this shiet.
            throw new NotImplementedException();
        }

        public int Add(Invitation invitationData)
        {
            this.invitations.Add(invitationData);
            this.invitations.SaveChanges();

            return invitationData.ID;
        }

        public int Remove(string email)
        {
            // TODO: Implement this shiet;
            throw new NotImplementedException();
        }

        public HttpStatusCode SendInvitation(AccountInvitationDataTransferModel invitationModel)
        {
            var validCommunity = this.communities.All().Any(x => x.Name == invitationModel.CommunityKey);

            if (!validCommunity)
            {
                return HttpStatusCode.Conflict;
            }

            // Checks if there is an invitation sent to this email already.
            Invitation existingInvitation = this.invitations.All().Where(x => x.Email == invitationModel.Email).FirstOrDefault();

            if (existingInvitation == null)
            {
                var token = this.GenerateVerificationToken() + invitationModel.CommunityKey;
                var invitation = new Invitation() { Email = invitationModel.Email, VerificationToken = token };

                this.Add(invitation);

                return this.SendEmail(invitationModel.Email, token);
            }
            else
            {
                var token = existingInvitation.VerificationToken;

                return this.SendEmail(invitationModel.Email, token);
            }
        }

        private string GenerateVerificationToken()
        {
            var generator = new RandomStringGenerator();

            var result = generator.GetString(Constants.VerificationTokenLength);

            return result;
        }

        private HttpStatusCode SendEmail(string email, string token)
        {
            var registrationURI = "https://neighbourscommunityclient/register/";
            var message = String.Format(CommunityConstants.RegistrationInvitationMessage,
                Environment.NewLine,
                registrationURI,
                token);

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator(CommunityConstants.MailgunAuthenticationApi, CommunityConstants.MailgunAuthenticationKey);

            RestRequest request = new RestRequest();
            request.Resource = "{domain}/messages";
            request.AddParameter("domain", "sandboxaa5c7c0d21a84aa1b0b2dd81fa9b283c.mailgun.org", ParameterType.UrlSegment);
            request.AddParameter("from", "Neighbours Community Management System <postmaster@sandboxaa5c7c0d21a84aa1b0b2dd81fa9b283c.mailgun.org>");
            request.AddParameter("to", String.Format("<{0}>", email));
            request.AddParameter("subject", "Hello, Neighbour!");
            request.AddParameter("text", message);
            request.Method = Method.POST;

            return ((RestResponse)client.Execute(request)).StatusCode;
        }
    }
}