namespace NeighboursCommunitySystem.Server.DataTransferModels.Proposals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Mapping;
    using Models;
    using Data.Repositories;
    using Data.DbContexts;

    public class VoteResponseModel : IMapFrom<Vote>, IHaveCustomMappings
    {
        private IRepository<User> repo = new EfGenericRepository<User>(new NeighboursCommunityDbContext());

        public string UserName { get; set; }

        public string Vote { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteResponseModel>()
                .ForMember(v => v.UserName, opt => opt.MapFrom(vo => repo.GetById(vo.UserId).FirstName))
                .ForMember(v => v.Vote, opt => opt.MapFrom(vo => vo.Option.ToString()));
        }
    }
}
