namespace NeighboursCommunitySystem.Server.DataTransferModels.Proposals
{
    using Models;

    public class ProposalDataTransferModel
    {
        public User Author { get; set; }

        public string Description { get; set; }

        public ushort Approvals { get; set; }
    }
}