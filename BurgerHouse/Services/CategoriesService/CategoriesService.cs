using Data;
using Data.Models;

namespace BurgerHouse.Services.CategoriesService
{
    public class CategoriesService : ICategoriesService
    {
        private ApplicationDbContext _context;

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public bool IsExistCategory(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public List<Item> GetCategoryItem(int categoryId)
        {
            return _context.Items.Where(x => x.CategoryId == categoryId).ToList();
        }

        public bool IsExistItem(int itemId)
        {
            return _context.Items.Any(x => x.Id == itemId);
        }

        public Item GetItem(int itemId)
        {
            return _context.Items.First(x => x.Id == itemId);
        }

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
