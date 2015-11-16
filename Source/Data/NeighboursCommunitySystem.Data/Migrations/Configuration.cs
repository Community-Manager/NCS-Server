namespace NeighboursCommunitySystem.Data.Migrations
{
    using DbContexts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<NeighboursCommunityDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "NeighboursCommunitySystem.Data.NeighboursCommunityDbContext";
        }

        protected override void Seed(NeighboursCommunityDbContext context)
        {
            // Communities
            var bulgarianCommunity = new Community() { Name = "BGSFSL152", Description = "Bulgaria`s first community group for the people of Sofia, Slatina, block 152, \"Ropotamo\" street." };
            var frenchCommunity = new Community() { Name = "FRPSCG14", Description = "France`s first community group in Paris, \"Charles de Gaule\", block 14." };
            var americanCommunity = new Community() { Name = "USNYNY7", Description = "USA`s first community group for the citizens of New York, 7th Skyscrapper." };

            context.Communities.AddOrUpdate(bulgarianCommunity, frenchCommunity, americanCommunity);

            // Voting options
            var optionFor = new VoteOption { Option = Options.For };
            var optionAgainst = new VoteOption { Option = Options.Against };
            var optionAbstention = new VoteOption { Option = Options.Abstention };

            context.VotingOptions.AddOrUpdate(optionFor, optionAgainst, optionAbstention);

            // Role manager
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var adminRole = new IdentityRole { Name = "Administrator" };
            var accountantRole = new IdentityRole { Name = "Accountant" };

            roleManager.Create(adminRole);
            roleManager.Create(accountantRole);

            // Admin and accountant for the Bulgarian community.
            var admin = new User()
            {
                UserName = "archer@gmail.com",
                Email = "archer@gmail.com",
                Id = "1",
                FirstName = "Archer",
                LastName = "Jr",
                PhoneNumber = "0887482921",
                ApartmentNumber = 1
            };

            var accountant = new User()
            {
                UserName = "cyril@gmail.com",
                Email = "cyril@gmail.com",
                Id = "2",
                FirstName = "Cyril",
                LastName = "Figgis",
                PhoneNumber = "0883333312",
                ApartmentNumber = 2
            };

            // Append roles
            userManager.Create(admin, "123456");
            userManager.AddToRole(admin.Id, "Administrator");

            userManager.Create(accountant, "123456");
            userManager.AddToRole(accountant.Id, "Accountant");

            // Append users to community
            bulgarianCommunity.Users.Add(admin);
            bulgarianCommunity.Users.Add(accountant);

            context.SaveChanges();
        }
    }
}