using BurgerHouse.Services.CategoriesService;
using BurgerHouse.Services.StopListService;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerHouse.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ICategoriesService _categoriesService;
        private IStopListService _stopListService;

        [HttpGet]
        public ActionResult<List<Category>> GetCategories() 
        {
            return Ok(_categoriesService.GetCategories());
        }

        [HttpGet("{categoryId}")]
        public ActionResult<List<Item>> GetItems(int categoryId) 
        {
            if (!_categoriesService.IsExistCategory(categoryId)) return BadRequest();

            return Ok(_categoriesService.GetCategoryItem(categoryId));

        }

        [HttpGet("getItem/{id}")]
        public ActionResult<Item> GetItem(int itemId)
        {
            if (!_categoriesService.IsExistItem(itemId)) return BadRequest();

            return Ok(_categoriesService.GetItem(itemId));
        }

        [HttpGet("stopList/{restrauntId}")]
        public ActionResult<IEnumerable<StopItem>> GetStopList(int restrauntId)
        {
            return Ok(_stopListService.GetStopList(restrauntId));

        }


        public ItemController(ICategoriesService categoriesService, IStopListService stopListService)
        {
            _categoriesService = categoriesService;
            _stopListService = stopListService;
        }
    }
}
