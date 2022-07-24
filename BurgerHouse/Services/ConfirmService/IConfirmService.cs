namespace BurgerHouse.Services.ConfirmService
{
    public interface IConfirmService
    {
        public void SendCode(int userId);

        public bool IsCodeRight(int userId, string code);
    }
}
