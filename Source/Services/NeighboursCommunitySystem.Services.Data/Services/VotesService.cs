namespace NeighboursCommunitySystem.Services.Data.Services
{
    using System.Linq;
    using Contracts;
    using Models;
    using NeighboursCommunitySystem.Data.Repositories;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;
        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }
        public IQueryable<Models.Vote> All()
        {
            return this.votesRepository.All();
        }
    }
}
