﻿using Data.Leyer.Models.Structs;
using goolrang_sales_v1.Models;

namespace Services.Leyer.Services.CategoryServices
{
    public interface ICategoryRepository
    {
        Task<Response<Category>> GetCategoryByName(string categoryName = null);
        Task<Response<Category>> GetAllCategory();
        Task<Response<Category>> DeleteCategory(int id);
        Task<Response<Category>> Test_Method();
    }
}