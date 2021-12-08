using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gamification
{
    public class AuthOptions
    {

        public const string Issuer = "AuthServer"; 
        public const string Audience = "AuthClient"; 
        const string Secret = "secret_secretkey!123"; 
        public const int TokenLifeTime = 10; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
