namespace NeighboursCommunitySystem.Server.DataTransferModels.Taxes
{
    using System;
    using Models;
    using Server.Common.Mapping;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using NeighboursCommunitySystem.Common;

    public class TaxDataTransferModel : IMapFrom<Tax>
    {
        private DateTime deadline;

        [Required]
        [MinLength(TaxesConstants.TaxesNameLengthMin, ErrorMessage = TaxesConstants.ShortNameErrorMessage)]
        [MaxLength(TaxesConstants.TaxesNameLengthMax, ErrorMessage = TaxesConstants.LongNameErrorMessage)]
        public string Name { get; set; }

        [MaxLength(TaxesConstants.DescriptionLengthMax, ErrorMessage = TaxesConstants.LongDescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Deadline
        {
            get
            {
                return this.deadline;
            }
            set
            {
                if (value.Date.CompareTo(DateTime.Now.AddDays(1).Date) <= 0)
                {
                    throw new ArgumentOutOfRangeException(TaxesConstants.DeadLineExceptionMessage);
                }

                this.deadline = value;
            }
        }
    }
}