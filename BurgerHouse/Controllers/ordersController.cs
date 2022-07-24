using BurgerHouse.Services.AuthService;
using BurgerHouse.Services.OrdersService;
using BurgerHouse.Services.WorkersService;
using Data.Models;
using Data.Models.NonDb;
using Data.Models.NonDb.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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
        public ActionResult<int> CreateOrder([FromBody] NonDbOrder createOrder)
        {
            var userId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id").Value);

            var order = _ordersService.CreateOrder(userId, createOrder);

           return Ok(order);
        }


        [Authorize]
        [HttpGet("{orderId}")]
        public ActionResult<ApiOrderReturn> GetOrder(int orderId)
        {
            var userId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id").Value);

            var order = _ordersService.GetOrder(orderId);
            if (order.UserId != userId) return BadRequest();
            var items = JsonConvert.DeserializeObject<List<OrderedItem>>(order.OrdererItemsAndCountJson);

            return Ok(
                
                new ApiOrderReturn() { MadePercent = order.MadePercent, OrderedItems =items, Price = order.ToPay, RestrauntId = order.RestrauntId, Id = order.Id }
                );
        }


        public ordersController(IOrdersService ordersService, IAuthService authService, IWorkersService workersService)
        {
            _ordersService = ordersService;
            _authService = authService;
            _workersService = workersService;
        }
    }
}
