using Gamification.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.IRepositories
{
    public interface IThankRepository
    {
        public Task<Thank> GetLastThankAsync(Guid currentUserId, CancellationToken cancellationToken);
        public Task<Thank> SayThankAsync(User currentUser, Thank newThank, CancellationToken cancellationToken);
    }
}
