using Gamification.BLL.DTO;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
        public Task<UserDTO> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken);
        public Task<User> CreateUserAsync(CreateUserDTO newUser, CancellationToken cancellationToken);
        public Task<UpdateUserDTO> UpdateUserAsync(Guid userId, UpdateUserDTO newUser, CancellationToken cancellationToken);
        public Task<User> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
        public Task<AuthenticationUserDTO> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken);
        public Task<AuthenticationUserDTO> GetUserByRefreshTokenAsync(Guid userId, CancellationToken cancellationToken);
        public Task<AuthenticationUserDTO> ChangePasswordAsync(string oldPassword, string newPassword, string confirmRassword, CancellationToken cancellationToken);
        public Task<IEnumerable<UserShortInfoDTO>> GetAllUsersWithLastAchievementAsync(CancellationToken cancellationToken);

    }
}
