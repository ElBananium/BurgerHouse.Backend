using BurgerHouse.Services.AuthService;
using BurgerHouse.Services.OrdersService;
using BurgerHouse.Services.StopListService;
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
    [Route("api/worker")]
    [ApiController]
    [Authorize(Policy = "OnlyForWorkers")]
    public class workerController : ControllerBase
    {

        private IOrdersService _ordersService;
        private IWorkersService _workersService;
        private IStopListService _stopListService;


        [HttpGet("setPercent/{orderId}/{percent}")]
        public ActionResult SetMadePercent(int orderId, int percent)
        {
            

            _ordersService.SetPercent(orderId, percent);

            return Ok();

            
        }


        [HttpGet("closeOrder/{orderId}")]
        public ActionResult CloseOrder(int orderId)
        {

            _ordersService.CloseOrder(orderId);

            return Ok();
            
        }

        
        [HttpPost("stoplist")]
        public ActionResult AddToStopList([FromBody] ApiStopItem stopItem)
        {


            var restrauntId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("RestrauntId").Value);

            _stopListService.AddToStopList(stopItem.restrauntId, stopItem.itemId);


            return Ok();
        }


        [HttpDelete("stoplist/{itemId}")]
        public ActionResult DeleteFromStopLsit(int itemId)
        {
            var restrauntId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("RestrauntId").Value);


            _stopListService.RemoveStopList(restrauntId,itemId);

            return Ok();
        }


        [HttpGet("getOrders")]
        public ActionResult<IEnumerable<ApiOrderReturn>> GetOrders()
        {


            var restrauntId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("RestrauntId").Value);

            var orders = _ordersService.GetOrders(restrauntId);

            var ordersToReturn = new List<ApiOrderReturn>();

            foreach(var order in orders)
            {
                var orderedItems = JsonConvert.DeserializeObject <List < OrderedItem >> (order.OrdererItemsAndCountJson);
                ordersToReturn.Add(new ApiOrderReturn() { MadePercent = order.MadePercent, Price = order.ToPay, RestrauntId = order.RestrauntId, OrderedItems = orderedItems });
            }

            return Ok(ordersToReturn);

        }


        public workerController(IOrdersService ordersService, IStopListService stopListService)
        {

            _ordersService = ordersService;

            _stopListService = stopListService;
        }
    }
}
