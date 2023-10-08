using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Category;
using goolrang_sales_v1.Models;

namespace Services.Leyer.Services.CategoryServices
{
    public interface ICategoryRepository
    {
        Task<Responses<Category>> GetCategoryByName(string categoryName = null);
        Task<Responses<Category>> GetAllCategory();
        Task<Responses<Category>> DeleteCategory(int id);
        Task<Responses<Category>> Test_Method();
        Task<Responses<Category>> CreateCategory(CreateCategoryVm categoryVm);
        Task<Responses<Category>> UpdateCategory(UpdateCategoryVm categoryVm);
    }   
}