using Data;
using Data.Models;
using Newtonsoft.Json;

namespace BurgerHouse.Services.OrdersService
{
    public class OrdersService : IOrdersService
    {
        private ApplicationDbContext _context;

        public int CreateOrder(List<int> itemsIds, int userId, int restrauntId)
        {

            var order = new Order() { MadePercent = 0, OrderedItemsIds = JsonConvert.SerializeObject(itemsIds), UserId = userId, RestrauntId = restrauntId};

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.First(x => x.Id == id);
        }

        public IEnumerable<Order> GetOrders(int RestrauntId)
        {
            return _context.Orders.Where(x => x.RestrauntId == RestrauntId).AsEnumerable();
        }

        public void ModifyOrder(int orderId, Order order)
        {
            _context.Orders.First(x => x.Id == orderId);

            order.Id = orderId;

            _context.Orders.Update(order);

            _context.SaveChanges();
        }

        public void CloseOrder(int orderId)
        {
            _context.Orders.Remove(new Order() { Id = orderId });
        }

        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
