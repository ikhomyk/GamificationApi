using Gamification.DAL.Repositories;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.IRepositories
{
    public interface IRequestAchievementRepository
    {
        public Task<RequestAchievement> AddRequest(RequestAchievement requestAchievement, CancellationToken cancellationToken);

    }
}
