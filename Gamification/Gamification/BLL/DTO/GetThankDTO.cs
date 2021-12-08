using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.BLL.DTO
{
    public class GetThankDTO
    {
        public string Text { get; set; }
        public Guid ToUserId { get; set; }
        public UserDTO FromUser { get; set; }
    }
}
