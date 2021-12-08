using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.IRepositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        public Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        public Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
        public Task<User> UpdateUserAsync(Guid userId, User user, CancellationToken cancellationToken);
        public Task<User> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
        public Task<User> AuthenticateUserAsync(string userName, string password, CancellationToken cancellationToken);
        public Task<User> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken);
        public Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
        public Task<User> GetUserByRefreshTokenAsync(Guid userId, CancellationToken cancellationToken);
        public Task<User> ChangePasswordAsync(string oldPassword, string newPassword, string confirmRassword, CancellationToken cancellationToken);
        public Task<IEnumerable<User>> GetAllUsersWithLastAchievementAsync(CancellationToken cancellationToken);
    }
}
