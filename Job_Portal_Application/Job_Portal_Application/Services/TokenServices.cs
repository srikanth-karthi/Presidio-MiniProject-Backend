using Job_Portal_Application.Models;
using Microsoft.IdentityModel.Tokens;
using static Job_Portal_Application.Services.TokenServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Services
{
    public class TokenServices: ITokenService
    {

     
              private readonly string _secretKey;
            private readonly SymmetricSecurityKey _key;

            public TokenServices(IConfiguration configuration)
            {
                _secretKey = configuration.GetSection("TokenKey").GetSection("JWT").Value.ToString();
                _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            }
            public string GenerateToken(Guid id, Roles role)
            {
                string token = string.Empty;
            var claims = new List<Claim>
                {
                    new Claim("id", id.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
                };
                var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var myToken = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
                token = new JwtSecurityTokenHandler().WriteToken(myToken);
                return token;
            }



        public bool VerifyPassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }


    }
}
