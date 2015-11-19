namespace NeighboursCommunitySystem.Server.DataTransferModels.Proposals
{
    using Common.Mapping;
    using Models;

    public class ProposalResponseModel : IMapFrom<Proposal>
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public string CommunityName { get; set; }
    }
}
