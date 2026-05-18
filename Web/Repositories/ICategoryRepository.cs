using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}
