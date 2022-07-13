using Data.Models;

namespace BurgerHouse.Services.CategoriesService
{
    public interface ICategoriesService
    {
        public List<Category> GetCategories();

        public bool IsExistCategory(int id);

        public List<Item> GetCategoryItem(int categoryId);
    }
}
