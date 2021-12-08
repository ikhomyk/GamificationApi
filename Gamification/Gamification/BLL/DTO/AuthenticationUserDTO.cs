using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamification.BLL.DTO
{
    public class AuthenticationUserDTO
    {
        public string Token { get; set; }
        [JsonIgnore]
        public Guid RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public bool IsAuthenticated { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public int Xp { get; set; }
        public int Badges { get; set; }
        public ICollection<RoleDTO> Roles { get; set; }
    }
}
