using BurgerHouse.Services.AuthService;
using BurgerHouse.Services.OrdersService;
using BurgerHouse.Services.WorkersService;
using Data.Models;
using Data.Models.NonDb.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BurgerHouse.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class ordersController : ControllerBase
    {
        private IOrdersService _ordersService;
        private IAuthService _authService;
        private IWorkersService _workersService;

        [Authorize]
        [HttpPost]
        public ActionResult<int> CreateOrder([FromBody] ApiOrderCreate createOrder)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());

            var order =  _ordersService.CreateOrder(createOrder.ItemsIds, info.UserId, createOrder.RestrauntId);

           return Ok(order);
        }


        [Authorize]
        [HttpGet("{orderId}")]
        public ActionResult<ApiOrderReturn> GetOrder(int orderId)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());
            var order = _ordersService.GetOrder(orderId);
            if (order.UserId != info.UserId) return BadRequest();
            var items = JsonConvert.DeserializeObject<List<int>>(order.OrderedItemsIds);

            return Ok(
                
                new ApiOrderReturn() { MadePercent = order.MadePercent, OrderedItems =items }
                );
        }
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());

            if (!info.CanViewOrders) return BadRequest();

            var restraunt = _workersService.GetRestrauntIdByWorkerId(info.UserId);


            return Ok(_ordersService.GetOrders(restraunt));

        }

        public ordersController(IOrdersService ordersService, IAuthService authService, IWorkersService workersService)
        {
            _ordersService = ordersService;
            _authService = authService;
            _workersService = workersService;
        }
    }
}
