namespace NeighboursCommunitySystem.Data.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DbContexts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;


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
            Community bulgarianCommunity = null;
            Community frenchCommunity = null;
            Community americanCommunity = null;

            if (!context.Communities.Any())
            {
                this.SeedCommunities(context, bulgarianCommunity, frenchCommunity, americanCommunity);
            }
            else
            {
                bulgarianCommunity = context.Communities.Single(x => x.Name == "BGSFSL152");
                frenchCommunity = context.Communities.Single(x => x.Name == "FRPSCG14");
                americanCommunity = context.Communities.Single(x => x.Name == "USNYNY7");
            }

            if (!context.VotingOptions.Any())
            {
                this.SeedVotingOptions(context);
            }

            if (!context.Roles.Any())
            {
                this.SeedUsersWithRoles(context, bulgarianCommunity, frenchCommunity, americanCommunity);
            }
        }

        private void SeedVotingOptions(INeighboursCommunityDbContext context)
        {
            var optionFor = new VoteOption { Option = Options.For };
            var optionAgainst = new VoteOption { Option = Options.Against };
            var optionAbstention = new VoteOption { Option = Options.Abstention };

            context.VotingOptions.AddOrUpdate(optionFor, optionAgainst, optionAbstention);

            context.SaveChanges();
        }

        private void SeedCommunities(INeighboursCommunityDbContext context, Community bulgarianCommunity, Community frenchCommunity, Community americanCommunity)
        {
            bulgarianCommunity = new Community() { Name = "BGSFSL152", Description = "Bulgaria`s first community group for the people of Sofia, Slatina, block 152, \"Ropotamo\" street." };
            frenchCommunity = new Community() { Name = "FRPSCG14", Description = "France`s first community group in Paris, \"Charles de Gaule\", block 14." };
            americanCommunity = new Community() { Name = "USNYNY7", Description = "USA`s first community group for the citizens of New York, 7th Skyscrapper." };

            context.Communities.AddOrUpdate(bulgarianCommunity, frenchCommunity, americanCommunity);

            context.SaveChanges();
        }

        private void SeedUsersWithRoles(DbContext context, Community bulgarianCommunity, Community frenchCommunity, Community americanCommunity)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var adminRole = new IdentityRole { Name = "Administrator" };

            roleManager.Create(adminRole);

            var bulgarianAdmin = new User()
            {
                UserName = "bulgarianAdmin@gmail.com",
                Email = "bulgarianAdmin@gmail.com",
                FirstName = "Sevar",
                LastName = "Georgiev",
                PhoneNumber = "0887482921",
                ApartmentNumber = 1
            };

            var frenchAdmin = new User()
            {
                UserName = "frenchadmin@gmail.com",
                Email = "frenchadmin@gmail.com",
                FirstName = "Contesse",
                LastName = "Dubois",
                PhoneNumber = "0887482921",
                ApartmentNumber = 1
            };

            var americanAdmin = new User()
            {
                UserName = "americanadmin@gmail.com",
                Email = "americanadmin@gmail.com",
                FirstName = "George",
                LastName = "Smith",
                PhoneNumber = "0887482921",
                ApartmentNumber = 1
            };

            userManager.Create(bulgarianAdmin, "123456");
            userManager.AddToRole(bulgarianAdmin.Id, "Administrator");

            userManager.Create(frenchAdmin, "123456");
            userManager.AddToRole(frenchAdmin.Id, "Administrator");

            userManager.Create(americanAdmin, "123456");
            userManager.AddToRole(americanAdmin.Id, "Administrator");

            if (!bulgarianCommunity.Users.Any())
            {
                bulgarianCommunity.Users.Add(bulgarianAdmin);
            }

            if (!frenchCommunity.Users.Any())
            {
                frenchCommunity.Users.Add(frenchAdmin);
            }

            if (!americanCommunity.Users.Any())
            {
                americanCommunity.Users.Add(americanAdmin);
            }

            context.SaveChanges();
        }
    }
}
