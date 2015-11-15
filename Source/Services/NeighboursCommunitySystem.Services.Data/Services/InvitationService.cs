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
    using DtoModels.Accounts;

    public class InvitationService : IInvitationService
    {
        private readonly IRepository<Invitation> invitations;

        public InvitationService(IRepository<Invitation> invitations)
        {
            this.invitations = invitations;
        }

        public IQueryable<Invitation> All()
        {
            return this.invitations.All().AsQueryable();
        }

        public IQueryable<Invitation> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public int Add(Invitation invitationData)
        {
            var invitation = new Invitation()
            {
                Email = invitationData.Email,
                VerificationToken = invitationData.VerificationToken,
                DecryptionKey = invitationData.DecryptionKey,
                InitializationVector = invitationData.InitializationVector,
            };

            this.invitations.Add(invitation);
            this.invitations.SaveChanges();

            return invitation.ID;
        }

        public int Remove(string email)
        {
            throw new NotImplementedException();
        }

        public string SendInvitation(AccountInvitationDataTransferModel invitationModel)
        {
            // Check if there is an invitation sent to this email already.
            var invitation = this.invitations.All().Where(x => x.Email == invitationModel.Email).FirstOrDefault();
            var statusDescription = string.Empty;

            if (invitation == null)
            {
                var token = this.GenerateVerificationToken() + invitationModel.CommunityKey;
                this.InsertInvitationDataInDatabase(invitationModel.Email, token);
                statusDescription = this.SendEmail(invitationModel.Email, token);
            }
            else
            {
                var token = this.DecryptStringFromBytes(
                    invitation.VerificationToken, 
                    invitation.DecryptionKey, 
                    invitation.InitializationVector) + invitationModel.CommunityKey;
                statusDescription = this.SendEmail(invitationModel.Email, token);
            }

            return statusDescription;
        }

        private string GenerateVerificationToken()
        {
            var generator = new RandomStringGenerator();

            var result = generator.GetString(Constants.VerificationTokenLength);

            return result;
        }

        private void InsertInvitationDataInDatabase(string email, string token)
        {
            var encryptedData = this.EncryptToken(email, token);

            if (encryptedData.Email == null)
            {
                throw new ArgumentNullException("Encryption not successful.");
            }

            this.Add(encryptedData);
        }

        private string SendEmail(string email, string token)
        {
            var registrationURI = "https://neighbourscms/register/";
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

            return ((RestResponse)client.Execute(request)).StatusDescription;
        }

        private Invitation EncryptToken(string email, string token)
        {
            var invitationData = new Invitation();

            try
            {
                // Create a new instance of the RijndaelManaged class.
                // This generates a new key and initialization vector (IV).
                using (RijndaelManaged rijndael = new RijndaelManaged())
                {
                    rijndael.GenerateKey();
                    rijndael.GenerateIV();

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes(token, rijndael.Key, rijndael.IV);

                    invitationData.Email = email;
                    invitationData.VerificationToken = encrypted;
                    invitationData.DecryptionKey = rijndael.Key;
                    invitationData.InitializationVector = rijndael.IV;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while encrypting the token: {0}", e.Message);
            }

            return invitationData;
        }

        private byte[] EncryptStringToBytes(string token, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (token == null || token.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");
            }

            byte[] encrypted;

            // Create a RijndaelManaged object with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(token);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private string DecryptStringFromBytes(byte[] tokenCipher, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (tokenCipher == null || tokenCipher.Length <= 0)
            {
                throw new ArgumentNullException("TokenCipher cannot be NULL.");
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key cannot be NULL");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");
            }

            // Declare the string used to hold the decrypted text.
            string result = null;

            // Create an RijndaelManaged object with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(tokenCipher))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }
    }
}