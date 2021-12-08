using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.BLL.DTO
{
    public class UserShortInfoDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Xp { get; set; }
        public Guid? AvatarId { get; set; }
        public ICollection<AchievementDTO> Achievements { get; set; }
    }
}
