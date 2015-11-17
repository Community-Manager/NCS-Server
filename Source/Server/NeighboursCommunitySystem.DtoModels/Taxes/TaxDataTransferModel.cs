namespace NeighboursCommunitySystem.DtoModels.Taxes
{
    using System;
    using Server.Common.Mapping;
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Common;
    using Server.Common.CustomAttributes;

    public class TaxDataTransferModel : IMapFrom<Tax>
    {
        [Required]
        [MinLength(TaxesConstants.TaxesNameLengthMin, ErrorMessage = TaxesConstants.ShortNameErrorMessage)]
        [MaxLength(TaxesConstants.TaxesNameLengthMax, ErrorMessage = TaxesConstants.LongNameErrorMessage)]
        public string Name { get; set; }

        [MaxLength(TaxesConstants.DescriptionLengthMax, ErrorMessage = TaxesConstants.LongDescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]        
        [ValidDateAttribute(ErrorMessage = TaxesConstants.DeadLineErrorMessage)]
        public DateTime Deadline { get; set; }
    }
}