using Azure;
using Dapper;
using Data.Leyer.DbContext;
using goolrang_sales_v1.Models;
using Newtonsoft.Json;
using System;
using Services.Leyer.ViewModels.ViewModels.Category;
using Services.Leyer.Responses.Structs;

namespace Services.Leyer.Services.CategoryServices;

public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _dbDapper;
    public CategoryRepository(MyDbContext dapperDb)
    {
        _dbDapper = dapperDb;
    }
    public async Task<Responses<Category>> GetAllCategory()
    {
        var query = "select * from Category";
        using( var connection = _dbDapper.CreateConnection())
        {
            var categories = await connection.QueryAsync<Category>(query);
            if(categories.Count() == 0)
            {
                return new Responses<Category>()
                {
                    ErrorCode = -100,
                    ErrorMessage = "we have no category yet",
                };
            }

            var jsonData = JsonConvert.SerializeObject(categories);

            return new Responses<Category>()
            {
                DataJson = jsonData,
                Data = categories
            };
        } 
    }
    public async Task<Responses<Category>> GetCategoryByName(string categoryName = null)
    {
        if (categoryName == null)
        {
            return new Responses<Category>()
            {
                ErrorCode = -150,
                ErrorMessage = "category Name is null",
            };
        }

        var query = "select * from Category" +
            $" where Category.CategoryName = '{categoryName}'";

        using (var connection = _dbDapper.CreateConnection())
        {
            var category = await connection.QueryAsync<Category>(query);
            if (category.Count() == 0 )
            {
                return new Responses<Category>()
                {
                    ErrorCode = -180,
                    ErrorMessage = "Category Not Found"
                };
            }

            var dataJson = JsonConvert.SerializeObject(category);
            return new Responses<Category>()
            {
                DataJson = dataJson,
                Data = category
            };
        }
    }
    public async Task<Responses<Category>> DeleteCategory( int id)
    {
        if( id < 0 )
        {
            return new Responses<Category>()
            {
                ErrorCode = -100,
                ErrorMessage = "ID is not correct",
            };
        }

        var query = $"category_delete_proc @CategoryId = {id} ";

        using (var connection = _dbDapper.CreateConnection())
        {
            var result = await connection.QueryAsync<int>(query);

            if(result.Any(x => x == -100))
            {
                return new Responses<Category>()
                {
                    ErrorCode = -100,
                    ErrorMessage = "this category is not exist",
                };
            }

            return new Responses<Category>();
        }
    }
    public async Task<Responses<Category>> CreateCategory(CreateCategoryVm categoryVm )
    {
        var response = await ValidateCreateVm(categoryVm);
        if (response.ErrorCode < 0)
            return response;

        var query = $"category_insert_proc @categoryName = {categoryVm.CategoryName.ToLower() } " +
            $" , @Description = {categoryVm.Description}";

        using (var connection = _dbDapper.CreateConnection())
        {
            var result = connection.QueryAsync(query);
        }

        return new Responses<Category>();
    }
    public async Task<Responses<Category>> UpdateCategory( UpdateCategoryVm categoryVm )
    {
        var response = await ValidateUpdateVm(categoryVm);
        if(response.ErrorCode < 0)
            return response;

        var query = $"category_update_proc @categoryId = {categoryVm.Id} ," +
            $" @categoryName = '{categoryVm.Name}', " +
            $" @description = '{categoryVm.Description}' ";

        using(var connection  = _dbDapper.CreateConnection())
        {
            var result = await connection.QueryAsync<int>(query);

            if(result.Any(x => x == -100))
            {
                response.ErrorCode = -100;
                response.ErrorMessage = "somthings Wrong";

                return response;
            }

            response.Message = $"Category: {categoryVm.Id} updated";
            return response;
        }
    }
    private async Task<Responses<Category>> ValidateCreateVm( CreateCategoryVm categoryVm)
    {
        if(categoryVm == null)
        {
            return new Responses<Category>()
            {
                ErrorCode = -125,
                ErrorMessage = "null data",
            };
        }
        else if( categoryVm.CategoryName == null || categoryVm.CategoryName.Length < 2 )
        {
            return new Responses<Category>()
            {
                ErrorCode = -135,
                ErrorMessage = "categoryName lenght is to short "
            };
        }
        else if(categoryVm.CategoryName.Length > 30)
        {
            return new Responses<Category>()
            {
                ErrorCode = -145,
                ErrorMessage = "categoryName lenght is to long "
            };
        }

        return new Responses<Category>();
    }
    private async Task<Responses<Category>> ValidateUpdateVm( UpdateCategoryVm categoryVm)
    {
        if (categoryVm == null)
        {
            return new Responses<Category>()
            {
                ErrorCode = -125,
                ErrorMessage = "null data",
            };
        }
        else if (categoryVm.Name == null || categoryVm.Name.Length < 2)
        {
            return new Responses<Category>()
            {
                ErrorCode = -135,
                ErrorMessage = "categoryName lenght is to short "
            };
        }
        else if (categoryVm.Name.Length > 30)
        {
            return new Responses<Category>()
            {
                ErrorCode = -145,
                ErrorMessage = "categoryName lenght is to long "
            };
        }

        return new Responses<Category>()
        {
            Message = "Done",
        };
    }
}
