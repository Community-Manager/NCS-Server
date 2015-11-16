namespace NeighboursCommunitySystem.Data.DbContexts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;

    public interface INeighboursCommunityDbContext
    {
        IDbSet<Tax> Taxes { get; set; }

        IDbSet<Proposal> Proposals { get; set; }

        IDbSet<Invitation> Invitations { get; set; }

        IDbSet<Community> Communities { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<VoteOption> VotingOptions { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}