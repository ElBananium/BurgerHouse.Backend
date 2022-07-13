namespace BurgerHouse.Services.ConfirmService
{
    public interface IConfirmService
    {
        public void SendLoginCode(int userId);

        public string LoginByCode(int userId, string code);



        public void SendConfirmCode(int userId);


        public void ConfirmByCode(int userId, string code);

        public bool IsConfirmedUser(int userId);
    }
}
