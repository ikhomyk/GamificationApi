using Gamification.DAL.IRepositories;
using Gamification.DAL.IRepository;
using Gamification.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAchievementRepository _achievementRepository;
        private IUserRepository _userRepository;
        private IThankRepository _thankRepository;
        private IRequestAchievementRepository _requestAchievementRepository;
        private readonly MyContext _context;

        public UnitOfWork(MyContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }
        public IAchievementRepository achievementRepository
        {
            get
            {
                if (this._achievementRepository == null)
                {
                    this._achievementRepository = new AchievementRepository(_context);
                }
                return _achievementRepository;
            }
            set
            {

            }
        }
        public IUserRepository userRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
            set
            {

            }
        }

        public IThankRepository thankRepository
        {
            get
            {
                if (this._thankRepository == null)
                {
                    this._thankRepository = new ThankRepository(_context);
                }
                return _thankRepository;
            }
            set
            {

            }
        }

        public IRequestAchievementRepository requestAchievementRepository
        {
            get
            {
                if (this._requestAchievementRepository == null)
                {
                    this._requestAchievementRepository = new RequestAchievementRepository(_context);
                }
                return _requestAchievementRepository;
            }
            set
            {

            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
