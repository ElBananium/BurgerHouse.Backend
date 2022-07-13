using Data;
using Data.Models;

namespace BurgerHouse.Services.StopListService
{
    public class StopListService : IStopListService
    {
        private ApplicationDbContext _context;

        public void AddToStopList(int restrauntId, int itemId)
        {
            _context.StopList.Add(new StopItem() { ItemId = itemId, RestrauntId = restrauntId });
            _context.SaveChanges();

            
        }

        public IEnumerable<StopItem> GetStopList(int restrauntId)
        {
            return _context.StopList.Where(x => x.RestrauntId == restrauntId).AsEnumerable();
        }

        public void RemoveStopList(int restrauntId, int itemId)
        {
            _context.StopList.Remove(new StopItem() { ItemId = itemId, RestrauntId = restrauntId });
            _context.SaveChanges(); 
        }

        public StopListService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
