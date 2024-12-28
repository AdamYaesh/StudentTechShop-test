using Microsoft.AspNetCore.Identity;
using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}
