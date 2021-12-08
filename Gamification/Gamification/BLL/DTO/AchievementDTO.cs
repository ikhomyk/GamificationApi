using System;

namespace Gamification.BLL.DTO
{
    public class AchievementDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Xp { get; set; }
        public string IconId { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
