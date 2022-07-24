using BurgerHouse.Services.AuthService;
using Data;

namespace BurgerHouse.Services.ConfirmService
{
    public class ConfirmService : IConfirmService
    {
        public bool IsCodeRight(int userId, string code)
        {
            return code == "123445";
        }

        public void SendCode(int userId)
        {
            return;
        }
    }
}
