using Data.Models;
using Data.Models.NonDb;

namespace BurgerHouse.Services.OrdersService
{
    public interface IOrdersService
    {

        public int CreateOrder(int userId, NonDbOrder order);

        public Order GetOrder(int id);

        public void SetPercent(int orderId, int percent);

        public void CloseOrder(int orderId);

        public IEnumerable<Order> GetOrders(int RestrauntId);
    }
}
