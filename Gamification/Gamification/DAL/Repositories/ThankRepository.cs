using Gamification.DAL.IRepositories;
using Gamification.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.Repositories
{
    public class ThankRepository : IThankRepository
    {
        private MyContext _context;

        public ThankRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<Thank> GetLastThankAsync(Guid currentUserId, CancellationToken cancellationToken)
        {
            Thank lastThank = await _context.Thanks.Include(u => u.FromUser).OrderByDescending(x => x.AddedTime).FirstOrDefaultAsync(x => x.ToUserId == currentUserId, cancellationToken);

            return lastThank;
        }

        public async Task<Thank> SayThankAsync(User currentUser, Thank newThank, CancellationToken cancellationToken)
        {
            Guid guid = Guid.NewGuid();
            newThank.FromUser = currentUser;
            newThank.Id = guid;
            newThank.AddedTime = DateTime.Now;
            await _context.Thanks.AddAsync(newThank, cancellationToken);
            await _context.SaveChangesAsync();

            return newThank;
        }
    }
}
