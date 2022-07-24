using Data.Models.NonDb;
using System.Security.Claims;

namespace BurgerHouse.Services.AuthService
{
    public interface IAuthService
    {
        public void RegistrateUser(string phoneNumber);

        public string GetUserToken(int userId);

        public bool isUserRegistratedAndConfirmed(string phoneNumber);

        public bool isUserExist(string phoneNumber);

        public void ConfirmUser(int userId);

        public int GetUserId(string phoneNumber);
    }
}
