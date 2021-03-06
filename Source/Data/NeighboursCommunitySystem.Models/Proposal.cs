﻿namespace NeighboursCommunitySystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Proposal
    {
        private ICollection<Vote> votes;

        public Proposal()
        {
            this.votes = new HashSet<Vote>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        public int CommunityId { get; set; }
        
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual Community Community { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }        
    }
}