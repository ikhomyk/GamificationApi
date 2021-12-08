using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.Models
{
    public class Achievement : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Xp { get; set; }
        [Required]
        public string IconId { get; set; }
        public DateTime AddedTime { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RequestAchievement> RequestAchievements { get; set; }
    }
}
