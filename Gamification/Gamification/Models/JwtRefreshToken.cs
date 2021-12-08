using Microsoft.EntityFrameworkCore;
using System;

namespace Gamification.Models
{
    [Owned]
    public class JwtRefreshToken 
    {
        public Guid RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
