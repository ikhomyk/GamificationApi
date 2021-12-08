using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.Models
{
    public class RequestAchievement
    {
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public User User { get; set; }
        public Achievement Achievement { get; set; }
        public string Message { get; set; }
        
    }
}
