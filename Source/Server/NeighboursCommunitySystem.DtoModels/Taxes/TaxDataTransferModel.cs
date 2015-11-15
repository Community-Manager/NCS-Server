namespace NeighboursCommunitySystem.DtoModels.Taxes
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Server.Common.Mapping;

    public class TaxDataTransferModel : IMapFrom<Tax>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime Deadline { get; set; }
    }
}