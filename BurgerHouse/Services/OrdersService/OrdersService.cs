using BurgerHouse.Services.CategoriesService;
using Data;
using Data.Models;
using Data.Models.NonDb;
using Newtonsoft.Json;

namespace BurgerHouse.Services.OrdersService
{
    public class OrdersService : IOrdersService
    {
        private ApplicationDbContext _context;
        private ICategoriesService _categories;

        public Order GetOrder(int id)
        {
            return _context.Orders.First(x => x.Id == id);
        }

        public IEnumerable<Order> GetOrders(int RestrauntId)
        {
            return _context.Orders.Where(x => x.RestrauntId == RestrauntId).AsEnumerable();
        }

        public void SetPercent(int orderId, int  percent)
        {
            var order = _context.Orders.First(x => x.Id == orderId);

            order.MadePercent = percent;

            

            _context.SaveChanges();
        }

        public void CloseOrder(int orderId)
        {
            _context.Orders.Remove(new Order() { Id = orderId });
            _context.SaveChanges();
        }

        public int CreateOrder(int userId, NonDbOrder order)
        {
            int topay = 0;

            
            order.OrderedItems.ForEach(x =>
            {
                topay+=x.Count * _categories.GetItem(x.ItemId).Price;

            });


            var dborder = new Order()
            {
                MadePercent = 0,
                RestrauntId = order.RestrauntId,
                UserId = userId,
                OrdererItemsAndCountJson = JsonConvert.SerializeObject(order.OrderedItems),
                ToPay = topay
            };

            _context.Orders.Add(dborder);

            _context.SaveChanges();

            return dborder.Id;
        }
        

        public OrdersService(ApplicationDbContext context, ICategoriesService categoriesService)
        {
            _context = context;
            _categories = categoriesService;
        }
    }
}
