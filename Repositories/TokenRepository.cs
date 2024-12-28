using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentTechShop.API.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentTechShop.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string CreateJWTToken(ApplicationUser user, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("UserId", user.Id));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
