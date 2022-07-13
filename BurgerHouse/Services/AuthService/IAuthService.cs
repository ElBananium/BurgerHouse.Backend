using Data.Models.NonDb;
using System.Security.Claims;

namespace BurgerHouse.Services.AuthService
{
    public interface IAuthService
    {
        public string RegistrateUser(string phoneNumber);

        public string GetUserToken(int userId);

        public bool CanRegistrateThisUser(string phoneNumber);

        public int GetUserId(string phoneNumber);

        public UserPermissions GetPermissions(List<Claim> claims);
    }
}
