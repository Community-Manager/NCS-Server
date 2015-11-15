namespace NeighboursCommunitySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(200)]
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
                    throw new ArgumentOutOfRangeException("The deadline must be atleast 24 hours further in the future counting from the current day.");
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