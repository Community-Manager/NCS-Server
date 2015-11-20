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
    using AutoMapper.QueryableExtensions;
    using Services.Data.Services;
    using Data.Repositories;

    [BreezeController]
    //[EnableCors(origins: "http://neighbourscommunityclient.azurewebsites.net, http://localhost:53074", headers: "*", methods: "*")]
    public class CommunitiesController : ApiController
    {
        private readonly ICommunitiesService communities;
        private ApplicationUserManager _userManager;

        public CommunitiesController(ICommunitiesService communities)
        {
            this.communities = communities;
        }

        // TODO: Ged rid of poor man's dependency inversion and find a library for using ninject with the tests.
        public CommunitiesController()
            : this (new CommunitiesService(new EfGenericRepository<Community>(new NeighboursCommunityDbContext())))
        {
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

        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            var result = communities.All()
                .Where(c => c.Users.Select(u => u.Id).Contains(id))
                .Select(c => new CommunityDataTransferModel()
                {
                    Name = c.Name,
                    Description = c.Description
                })

                .ToList();

            return this.Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult PostCommunityByLoggedAdmin(CommunityDataTransferModel model)
        {
            // Check if community with the same name already exists.
            if (this.communities.All().Any(c => c.Name == model.Name))
            {
                return this.BadRequest("Community model with the same name already exists.");
            }

            var newCommunityId = communities.Add(model);

            return this.Ok(newCommunityId);
        }


        [ValidateModel]
        public async Task<IHttpActionResult> Post(CommunityWithAdminDataTransferModel model)
        {
            // Check if community with the same name already exists.
            if (this.communities.All().Any(c => c.Name == model.CommunityModel.Name))
            {
                return this.Ok("Community model with the same name already exists.");
            }

            // Create new community and store it in the database. 
            var newCommunityId = communities.Add(model.CommunityModel);
            var tokenGenerator = new RandomStringGenerator();
            var token = tokenGenerator.GetString(Server.Common.Constants.Constants.VerificationTokenLength) + model.CommunityModel.Name;

            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ApartmentNumber = model.ApartmentNumber
            };

            // Create user and append Administator role.
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            UserManager.AddToRole(user.Id, "Administrator");

            if (!result.Succeeded)
            {
                // Delete community from database.
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