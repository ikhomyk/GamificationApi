using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamification.Models
{
    public class Role : BaseEntity
    {
        [Required]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
