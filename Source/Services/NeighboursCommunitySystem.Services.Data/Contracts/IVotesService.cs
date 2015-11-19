namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    using System;
    using System.Linq;
    using Models;

    public interface IVotesService : IService
    {
        IQueryable<Vote> All();
    }
}
