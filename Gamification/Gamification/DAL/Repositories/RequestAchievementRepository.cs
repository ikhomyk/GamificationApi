using Gamification.DAL.IRepositories;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.Repositories
{
    public class RequestAchievementRepository : IRequestAchievementRepository
    {
        public readonly MyContext _context;
        public RequestAchievementRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<RequestAchievement> AddRequest(RequestAchievement requestAchievement, CancellationToken cancellationToken)
        {
            await _context.RequestAchievements.AddAsync(requestAchievement, cancellationToken);
            await _context.SaveChangesAsync();
            return requestAchievement;
        }
    }
}
