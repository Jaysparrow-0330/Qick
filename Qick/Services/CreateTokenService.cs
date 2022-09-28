using Microsoft.IdentityModel.Tokens;
using Qick.Dto.Enum;
using Qick.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Qick.Services
{
    public class CreateTokenService
    {
        private readonly SymmetricSecurityKey _key;

        public CreateTokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Authentication:JWTKey"]));
        }

        public string CreateToken(User user, bool isUpdate, DateTime? updateDay)
        {
            try
            {
                DateTime expires = DateTime.Now;
                if (user.Role.Equals(Roles.ADMIN) || user.Role.Equals(Roles.MANAGER) || user.Role.Equals(Roles.GOD) || user.Role.Equals(Roles.PSY) || user.Role.Equals(Roles.STAFF))
                {
                    expires = expires.AddDays(7);
                }
                else if (user.Role.Equals(Roles.MEMBER))
                {
                    expires = expires.AddDays(7);
                }
                else
                {
                    throw new InvalidOperationException();
                }
                if (isUpdate)
                {
                    expires = (DateTime)updateDay;
                }
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim("imageUrl", user.ImageUrl),
                //new Claim("userName", user.Name),
                new Claim("status", user.Status.ToString())
            };
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDes = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expires,
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDes);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateTokenTest(User user, double min)
        {
            try
            {
                DateTime expires = DateTime.Now;
                expires = expires.AddMinutes(min);
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim("imageUrl", user.ImageUrl),
                //new Claim("userName", user.Name),
                new Claim("status", user.Status.ToString())
            };
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDes = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expires,
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDes);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
