namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System;
    using System.Linq;
    using Models;
    using Contracts;
    using NeighboursCommunitySystem.Data.Repositories;

    // Under construction.
    public class CommunitiesService : ICommunitiesService
    {
        private readonly IRepository<Community> communities;

        public CommunitiesService(IRepository<Community> dbCommunities)
        {
            this.communities = dbCommunities;
        }

        public int Add(string name, string description = null)
        {
            var community = new Community
            {
                Name = name,
                Description = description
            };

            communities.Add(community);
            communities.SaveChanges();

            return community.Id;
        }

        public Community GetById(int id)
        {
            return communities.GetById(id);
        }

        public IQueryable<Community> All()
        {
            return communities.All();
        }

        public IQueryable<Community> ByCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
