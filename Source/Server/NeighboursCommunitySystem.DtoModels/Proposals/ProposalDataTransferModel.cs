namespace NeighboursCommunitySystem.Server.DataTransferModels.Proposals
{
    using Common.Mapping;
    using Models;

    public class ProposalDataTransferModel : IMapFrom<Proposal>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public string CommunityName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Proposal, ProposalDataTransferModel>()
                .ForMember(m => m.CommunityName, opt => opt.Ignore())
                .ForMember(m => m.Description, opt => opt.MapFrom(pr => pr.Description))
                .ForMember(m => m.Title, opt => opt.MapFrom(pr => pr.Title));

        }
    }
}