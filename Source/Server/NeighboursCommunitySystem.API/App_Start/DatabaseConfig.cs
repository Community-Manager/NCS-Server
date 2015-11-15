namespace NeighboursCommunitySystem.API
{
    using System.Data.Entity;
    using Data.DbContexts;
    using Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NeighboursCommunityDbContext, Configuration>());
            NeighboursCommunityDbContext.Create().Database.Initialize(true);
        }
    }
}