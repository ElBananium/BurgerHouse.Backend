using BurgerHouse.Services.AuthService;
using BurgerHouse.Services.OrdersService;
using BurgerHouse.Services.StopListService;
using BurgerHouse.Services.WorkersService;
using Data.Models;
using Data.Models.NonDb.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerHouse.Controllers
{
    [Route("api/worker")]
    [ApiController]
    public class workerController : ControllerBase
    {
        private IAuthService _authService;

        private IOrdersService _ordersService;
        private IWorkersService _workersService;
        private IStopListService _stopListService;

        [Authorize]
        [HttpGet("setPercent/{orderId}/{percent}")]
        public ActionResult SetMadePercent(int orderId, int percent)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());

            if (!info.CanViewOrders) return BadRequest();

            _ordersService.ModifyOrder(orderId, new Order() { MadePercent = percent });

            return Ok();

            
        }

        [Authorize]
        [HttpGet("closeOrder/{orderId}")]
        public ActionResult CloseOrder(int orderId)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());

            if (!info.CanViewOrders) return BadRequest();

            _ordersService.CloseOrder(orderId);

            return Ok();
            
        }

        [Authorize]
        [HttpPost("stoplist")]
        public ActionResult AddToStopList([FromBody] ApiStopItem stopItem)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());

            if (info.CanViewOrders) return BadRequest();



            var restrauntId = _workersService.GetRestrauntIdByWorkerId(info.UserId);

            _stopListService.AddToStopList(stopItem.restrauntId, stopItem.itemId);


            return Ok();
        }

        [Authorize]
        [HttpDelete("stoplist/{itemId}")]
        public ActionResult DeleteFromStopLsit(int itemId)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());

            if (info.CanViewOrders) return BadRequest();



            var restrauntId = _workersService.GetRestrauntIdByWorkerId(info.UserId);


            _stopListService.RemoveStopList(restrauntId,itemId);

            return Ok();
        }


        public workerController(IAuthService authService, IWorkersService workersService, IOrdersService ordersService, IStopListService stopListService)
        {
            _authService = authService;
            _ordersService = ordersService;
            _workersService = workersService;
            _stopListService = stopListService;
        }
    }
}
