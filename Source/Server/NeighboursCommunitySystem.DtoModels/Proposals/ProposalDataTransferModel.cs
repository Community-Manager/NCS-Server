namespace NeighboursCommunitySystem.Server.DataTransferModels.Proposals
{
    using Common.Mapping;
    using Models;

    public class ProposalDataTransferModel : IMapFrom<Proposal>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public string CommunityName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ProposalDataTransferModel, Proposal>()
                .ForMember(m => m.AuthorId, opt => opt.MapFrom(pr => pr.UserId))
                .ForMember(m => m.Community, opt => opt.MapFrom(pr => pr.CommunityName))
                .ForMember(m => m.Description, opt => opt.MapFrom(pr => pr.Description))
                .ForMember(m => m.Title, opt => opt.MapFrom(pr => pr.Title));

        }
    }
}