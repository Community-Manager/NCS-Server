﻿namespace NeighboursCommunitySystem.Data.DbContexts
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class NeighboursCommunityDbContext : IdentityDbContext<User>, INeighboursCommunityDbContext
    {
        public NeighboursCommunityDbContext()
            : base("NeighboursCommunitySystem", throwIfV1Schema: false)
        {
            // Configuration.ProxyCreationEnabled = false;
            // Configuration.LazyLoadingEnabled = false;
        }
        
        public virtual IDbSet<Tax> Taxes { get; set; }

        public virtual IDbSet<Proposal> Proposals { get; set; }

        public virtual IDbSet<Invitation> Invitations { get; set; }

        public virtual IDbSet<Community> Communities { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public virtual IDbSet<VoteOption> VotingOptions { get; set; }

        public static NeighboursCommunityDbContext Create()
        {
            return new NeighboursCommunityDbContext();
        }
    }
}