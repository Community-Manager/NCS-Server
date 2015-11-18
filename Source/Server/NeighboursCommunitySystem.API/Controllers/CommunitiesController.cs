namespace NeighboursCommunitySystem.API.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Breeze.WebApi2;
    using Common;
    using Server.DataTransferModels.Communities;
    using Data.DbContexts;
    using Server.Infrastructure.Validation;
    using Server.Common.Generators;
    using Server.Common.Constants;
    using Microsoft.AspNet.Identity;
    using System.Net.Http;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Contracts;
    using Models;

    [BreezeController]
    //[EnableCors(origins: "http://neighbourscommunityclient.azurewebsites.net, http://localhost:53074", headers: "*", methods: "*")]
    public class CommunitiesController : ApiController
    {
        // TODO: Remove and Delete methods, Delete will set IsDeleted to True, Remove will remove the data entirely     
        private readonly ICommunitiesService communities;
        private ApplicationUserManager _userManager;

        public CommunitiesController(ICommunitiesService communities)
        {
            this.communities = communities;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [EnableBreezeQuery]
        public IHttpActionResult Get()
        {
            var result = communities.All()
                .Select(c => new CommunityDataTransferModel()
                {
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            return this.Ok(result);
        }

        [ValidateModel]
        public async Task<IHttpActionResult> Post(CommunityWithAdminDataTransferModel model)
        {
            // Check if community with the same name already exists.
            if (this.communities.All().Any(c => c.Name == model.CommunityModel.Name))
            {
                return this.Ok("Community model with the same name already exists.");
            }

            // Create new community and send it to the database.
            var newCommunityId = communities.Add(model.CommunityModel);
           
            // Create user and append Administator role.
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ApartmentNumber = model.ApartmentNumber
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            UserManager.AddToRole(user.Id, "Administrator");

            // If administrator with the following credentials cannot be created,
            // Delete community from the database and return response indicating failure.
            if (!result.Succeeded)
            {
                this.communities.RemoveById(newCommunityId);
                return this.BadRequest("User creation failed.");
            }

            // Append user to the specified community.
            var communityName = model.CommunityModel.Name;

            this.communities.All()
                .Where(x => x.Name == communityName)
                .FirstOrDefault()
                .Users
                .Add(user);

            return this.Ok();
        }
    }
}