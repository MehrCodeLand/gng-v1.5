using Azure;
using Dapper;
using Data.Leyer.DbContext;
using goolrang_sales_v1.Models;
using Newtonsoft.Json;
using System;
using Services.Leyer.ViewModels.ViewModels.Category;
using Services.Leyer.Responses.Structs;
using Services.Leyer.Localization;
using Services.Leyer.ViewModels.Category;
using System.Linq;

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
                    HasError = true,
                    ErrorMessage = Messages.NoDataReult 
                };
            }

            return new Responses<Category>()
            {
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
                HasError = true ,
                ErrorMessage = Messages.InvalidInputData
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
                    HasError = true ,
                    ErrorMessage = Messages.RecordNotFound
                };
            }

            var dataJson = JsonConvert.SerializeObject(category);
            return new Responses<Category>()
            {
                Data = category
            };
        }
    }
    public async Task<Responses<Category>> DeleteCategory( int id)
    {
        if( id <= 0 )
        {
            return new Responses<Category>()
            {
                HasError = true,
                ErrorMessage = Messages.InvalidInputData
            };
        }

        var query = $" exec delete_category_proc @categoryId = {id}";

        using (var connection = _dbDapper.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Any(x => x.ErrorCode != null ))
            {
                if( result.Any(x => x.ErrorCode == -100))
                {
                    return new Responses<Category>()
                    {
                        HasError = true,
                        ErrorMessage = Messages.FKeror
                    };
                }
                else if(result.Any(x => x.ErrorCode == -300) )
                {
                    return new Responses<Category>()
                    {
                        HasError = true,
                        ErrorMessage = Messages.NoDataReult
                    };
                }

                return new Responses<Category>()
                {
                    HasError = true,
                    ErrorMessage = Messages.RecordNotFound 
                };
            }

            return new Responses<Category>();
        }
    }
    public async Task<Responses<Category>> CreateCategory(CreateCategoryVm categoryVm )
    {
        var query = $"exec insert_category_adv_proc @Name = '{categoryVm.CategoryName}' , " +
            $" @Description = '{categoryVm.Description}' ";

        using (var connection = _dbDapper.CreateConnection())
        {
            var result =await connection.QueryAsync(query);
        }

        return new Responses<Category>();
    }
    public async Task<Responses<Category>> UpdateCategory(int id , UpdateCategoryVm categoryVm )
    {
        var query = $"category_update_proc @categoryId = {id} ," +
            $" @categoryName = '{categoryVm.CategoryName}', " +
            $" @description = '{categoryVm.Description}' ";

        using(var connection  = _dbDapper.CreateConnection())
        {
            var result = await connection.QueryAsync<int>(query);

            if(result.Any(x => x == -100))
            {
                return new Responses<Category>()
                {
                    HasError = true,
                    ErrorMessage = Messages.SomthingWrong
                };
            }

            return new Responses<Category>();
        }
    }
}
