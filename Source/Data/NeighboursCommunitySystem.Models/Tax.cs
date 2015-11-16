namespace NeighboursCommunitySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;

    public class Tax
    {
        private ICollection<TaxPayment> payments;
        private DateTime deadline;

        public Tax()
        {
            this.payments = new HashSet<TaxPayment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(TaxesConstants.TaxesNameLengthMin)]
        [MaxLength(TaxesConstants.TaxesNameLengthMax)]
        public string Name { get; set; }

        [MaxLength(TaxesConstants.DescriptionLengthMax)]
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

        public int CommunityId { get; set; }

        public virtual Community Community { get; set; }

        public virtual ICollection<TaxPayment> Payments
        {
            get { return this.payments; }
            set { this.payments = value; }
        }
    }
}