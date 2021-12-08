using System;
using System.Collections.Generic;

namespace Gamification.BLL.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int Xp { get; set; }
        public Guid? AvatarId { get; set; }
        public ICollection<RoleDTO> Roles { get; set; }
        public ICollection<AchievementDTO> Achievements { get; set; }
    }
}
