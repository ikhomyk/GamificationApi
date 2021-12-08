using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamification.Models
{
    public class Thank : BaseEntity
    {
        [Required]
        public Guid ToUserId { get; set; }

        //public Guid FromUserId { get; set; }
        public User FromUser { get; set; }

        [Required]
        [StringLength(100)]
        public string Text { get; set; }

        public DateTime AddedTime { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
