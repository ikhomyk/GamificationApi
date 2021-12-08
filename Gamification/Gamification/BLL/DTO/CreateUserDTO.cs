using System.Collections.Generic;

namespace Gamification.BLL.DTO
{
    public class CreateUserDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public ICollection<RoleDTO> Roles { get; set; }
    }
}
