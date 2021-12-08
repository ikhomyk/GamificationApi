using Gamification.DAL.IRepositories;
using Gamification.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MyContext _context;

        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Include(a => a.Roles)
                .Include(b => b.Achievements)
                .ToListAsync(cancellationToken);

            foreach (User user in users)
            {
                int totalXp = user.Achievements.Sum(x => x.Xp);
                user.Xp = totalXp;
                user.Badges = user.Achievements.Count();
            }

            return users.OrderByDescending(c => c.Xp);
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await _context.Users
                .Include(a => a.Roles)
                .Include(b => b.Achievements)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            int totalXp = user.Achievements.Sum(x => x.Xp);
            user.Xp = totalXp;
            user.Badges = user.Achievements.Count();

            return user;
        }

        public async Task<User> CreateUserAsync(User newUser, CancellationToken cancellationToken)
        {
            Guid guid = Guid.NewGuid();

            newUser.Id = guid;

            await _context.Users.AddAsync(newUser, cancellationToken);

            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> UpdateUserAsync(Guid userId, User newUser, CancellationToken cancellationToken)
        {
            User user = await _context.Users.Include(x => x.JwtRefreshTokens)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user != null)
            {
                user.JwtRefreshTokens.Clear();
                user.JwtRefreshTokens = newUser.JwtRefreshTokens;
                user.Password = user.Password;
                user.Id = userId;
                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Email = newUser.Email;
                user.Status = newUser.Status;
                user.Roles = user.Roles;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            _context.Users.Attach(user);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> AuthenticateUserAsync(string userName, string password, CancellationToken cancellationToken)
        {
            User authenticatedUser = await _context.Users.Include(x => x.Achievements).Include(a => a.Roles).FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password, cancellationToken);

            int totalXp = authenticatedUser.Achievements.Sum(x => x.Xp);
            authenticatedUser.Xp = totalXp;
            authenticatedUser.Badges = authenticatedUser.Achievements.Count();

            return authenticatedUser;
        }

        public async Task<User> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await GetUserByIdAsync(userId, cancellationToken);

            return user;
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user != null)
            {
                var roles = user.Roles;
                return roles;
            }

            return null;
        }

        public async Task<User> GetUserByRefreshTokenAsync(Guid refreshTokenId, CancellationToken cancellationToken)
        {
            User user = await _context.Users
                .Include(x => x.Roles)
                .Include(x => x.JwtRefreshTokens)
                .FirstOrDefaultAsync(u => u.JwtRefreshTokens
                    .Any(t => t.RefreshToken == refreshTokenId), cancellationToken);

            return user;
        }

        public async Task<User> ChangePasswordAsync(string oldPassword, string newPassword, string confirmRassword, CancellationToken cancellationToken)
        {
            if (newPassword == confirmRassword)
            {
                User user = await _context.Users
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.Password == oldPassword, cancellationToken);

                user.Password = newPassword;
                User newUser = await UpdateUserAsync(user.Id, user, cancellationToken);

                return newUser;
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetAllUsersWithLastAchievementAsync(CancellationToken cancellationToken)
        {
            var users = _context.Users.Include(a => a.Achievements.OrderByDescending(d => d.AddedTime).Take(1));

            return await users.ToListAsync(cancellationToken);
        }
    }
}

