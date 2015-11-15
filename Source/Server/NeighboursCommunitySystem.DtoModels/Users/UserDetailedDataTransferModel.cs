namespace NeighboursCommunitySystem.DtoModels.Users
{
    using Models;
    using System.Collections.Generic;

    public class UserDetailedDataTransferModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte ApartmentNumber { get; set; }

        public IEnumerable<Tax> Taxes { get; set; }

        public IEnumerable<Proposal> Proposals { get; set; }
    }
}