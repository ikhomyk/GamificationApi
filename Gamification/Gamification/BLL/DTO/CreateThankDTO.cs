using System;

namespace Gamification.DAL.Repositories
{
    public class CreateThankDTO
    {
        public string Text { get; set; }
        public Guid ToUserId { get; set; }

    }
}
