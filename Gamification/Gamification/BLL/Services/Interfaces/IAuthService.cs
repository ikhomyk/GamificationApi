using Gamification.BLL.DTO;
using Gamification.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthenticationUserDTO> AuthenticateAsync(string password, string email, CancellationToken cancellationToken);
        public Task<User> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken);
        public Task<AuthenticationUserDTO> RefreshTokenAsync(Guid tokenId, CancellationToken cancellationToken);
    }
}
