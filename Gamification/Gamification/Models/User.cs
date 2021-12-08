using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamification.Models
{
    public class User : BaseEntity
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [StringLength(60, ErrorMessage = "User firstname cannot be longer that 60 characters")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [StringLength(60, ErrorMessage = "User lastname cannot be longer that 60 characters")]
        public string LastName { get; set; }

        [StringLength(60, ErrorMessage = "User lastname cannot be longer that 60 characters")]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])$", ErrorMessage = "Password must meet requirements")]
        public string Password { get; set; }
        public string Status { get; set; }

        public int Xp { get; set; }
        public int Badges { get; set; }

        public string AvatarId { get; set; }

        public ICollection<Role> Roles { get; set; }

        public ICollection<Achievement> Achievements { get; set; }
        public Thank Thank { get; set; }
        public ICollection<JwtRefreshToken> JwtRefreshTokens { get; set; }
        public ICollection<RequestAchievement> RequestAchievements { get; set; }
        public string Token { get; set; }
    }
}
