using Data.Models;

namespace BurgerHouse.Services.OrdersService
{
    public interface IOrdersService
    {

        public int CreateOrder(List<int> itemsIds, int userId, int restrauntId);

        public Order GetOrder(int id);

        public void ModifyOrder(int orderId,Order order);

        public void CloseOrder(int orderId);

        public IEnumerable<Order> GetOrders(int RestrauntId);
    }
}
