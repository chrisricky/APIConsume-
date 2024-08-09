using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewAPIConsume.AuthClass
{
    public class JWTRepository: IJWTRepository
    {
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
        {
                { "admin","password"}
        };
        private readonly IConfiguration iconfiguration;
        public JWTRepository(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }


        public Tokens Authenticate(Users users)
        {

           
            if (!UsersRecords.Any(x => x.Key == users.username && x.Value == users.password))
            {
                return null;
            }



            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, users.username),
              new Claim("Role", "admin")
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

        }

    }
}
