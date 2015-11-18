namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Models;
    using Contracts;
    using NeighboursCommunitySystem.Data.Repositories;
    using Server.DataTransferModels.Communities;
    using System;

    // Under construction.
    public class CommunitiesService : ICommunitiesService
    {
        private readonly IRepository<Community> communities;

        public CommunitiesService(IRepository<Community> dbCommunities)
        {
            this.communities = dbCommunities;
        }

        public int Add(CommunityDataTransferModel model)
        {
            var community = new Community
            {
                Name = model.Name,
                Description = model.Description
            };

            this.communities.Add(community);
            this.communities.SaveChanges();

            return community.Id;
        }

        public Community GetById(int id)
        {
            return this.communities.GetById(id);
        }

        public IQueryable<Community> All()
        {
            return this.communities.All();
        }

        public bool Remove(CommunityDataTransferModel model)
        {
            var community = this.communities.All().Where(x => x.Name == model.Name).FirstOrDefault();

            if (community != null)
            {
                this.communities.Delete(community);
                this.communities.SaveChanges();

                return true;
            }

            return false;
        }

        public bool RemoveById(int id)
        {
            var community = this.communities.All()
                .Where(x => x.Id == id)
                .ProjectTo<CommunityDataTransferModel>()
                .FirstOrDefault();

            var isRemoved = this.Remove(community);

            return isRemoved;
        }

        public int Update(CommunityDataTransferModel model)
        {
            throw new NotImplementedException();
        }
    }
}