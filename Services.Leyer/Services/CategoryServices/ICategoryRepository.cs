using goolrang_sales_v1.Models;
using Services.Leyer.Responses.Structs;
using Services.Leyer.ViewModels.Category;
using Services.Leyer.ViewModels.ViewModels.Category;

namespace Services.Leyer.Services.CategoryServices;

public interface ICategoryRepository
{
    Task<Responses<Category>> GetCategoryByName(string categoryName = null);
    Task<Responses<Category>> GetAllCategory();
    Task<Responses<Category>> DeleteCategory(int id);
    Task<Responses<Category>> CreateCategory(CreateCategoryVm categoryVm);
    Task<Responses<Category>> UpdateCategory( int id , UpdateCategoryVm categoryVm);
}   