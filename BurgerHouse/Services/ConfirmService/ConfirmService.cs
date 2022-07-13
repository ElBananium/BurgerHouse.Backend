using BurgerHouse.Services.AuthService;
using Data;

namespace BurgerHouse.Services.ConfirmService
{
    public class ConfirmService : IConfirmService
    {
        private ApplicationDbContext _context;
        private IAuthService _authService;

        public void ConfirmByCode(int userId, string code)
        {
            if (code != userId.ToString() + "sas") return;

            _context.Users.First(x => x.Id == userId).IsConfirmed = true;

            _context.SaveChanges();
        }

        public void SendConfirmCode(int userId)
        {
            return;
        }

        public bool IsConfirmedUser(int userId)
        {
            return _context.Users.First(x => x.Id == userId).IsConfirmed;
        }

        public void SendLoginCode(int userId)
        {
            return;
        }

        public string LoginByCode(int userId, string code)
        {
            if (code != userId.ToString() + "sas") return "0";

            return _authService.GetUserToken(userId);


        }

        public ConfirmService(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
    }
}
