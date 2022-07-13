using Data.Models;

namespace BurgerHouse.Services.StopListService
{
    public interface IStopListService
    {
        public IEnumerable<StopItem> GetStopList(int restrauntId);


        public void AddToStopList(int restrauntId, int itemId);

        public void RemoveStopList(int restrauntId, int itemId);

    }
}
