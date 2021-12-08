using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.BLL.DTO
{
    public class RequestAchievementDTO
    {
        public Guid AchievementId { get; set; }
        public string Message { get; set; }
    }
}
