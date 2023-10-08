using Dapper;
using Data.Leyer.DbContext;
using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Category;
using goolrang_sales_v1.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Services.Leyer.Services.CategoryServices;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperDbContext _dbDapper;
    public CategoryRepository(DapperDbContext dapperDb)
    {
        _dbDapper = dapperDb;
    }



    #region Read
    public async Task<Response<Category>> GetAllCategory()
    {
        var query = "select * from Category";
        using( var connection = _dbDapper.CreateConnection())
        {
            var categories = await connection.QueryAsync<Category>(query);
            if(categories.Count() == 0)
            {
                return new Response<Category>()
                {
                    ErrorCode = -100,
                    ErrorMessage = "we have no category yet",
                };
            }

            var jsonData = JsonConvert.SerializeObject(categories);

            return new Response<Category>()
            {
                DataJson = jsonData,
                Data = categories ,
                Message = $"We found {categories.Count()} categoriy/ies"
            };
        } 

    }
    public async Task<Response<Category>> GetCategoryByName(string categoryName = null)
    {
        if (categoryName == null)
        {
            return new Response<Category>()
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
                return new Response<Category>()
                {
                    ErrorCode = -180,
                    ErrorMessage = "Category Not Found"
                };
            }

            var dataJson = JsonConvert.SerializeObject(category);
            return new Response<Category>()
            {
                Message = "Category is find",
                DataJson = dataJson,
                Data = category
            };
        }
    }

    #endregion

    #region Delete

    public async Task<Response<Category>> DeleteCategory( int id)
    {
        if( id < 0 )
        {
            return new Response<Category>()
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
                return new Response<Category>()
                {
                    ErrorCode = -100,
                    ErrorMessage = "this category is not exist",
                };
            }

            return new Response<Category>()
            {
                Message = $"category {id} was deleted.",
            };

        }

    }

    #endregion

    #region Create

    public async Task<Response<Category>> CreateCategory(CreateCategoryVm categoryVm )
    {



        return new Response<Category>()
        {

        };
    }

    #endregion



    #region Validation

    public async Task<Response<Category>> ValidateCreateVm( CreateCategoryVm categoryVm)
    {



        return new Response<Category>() { };
    }
    #endregion


    #region Test Method
    public async Task<Response<Category>> Test_Method()
    {
        var query = "if exists (select 1 from Category where CategoryId = 4 )" +
            " begin select 100 end ";

        using (var conection = _dbDapper.CreateConnection())
        {
            var result = await conection.QueryAsync<int>(query);

            int i = 100;
        }



        return new Response<Category>() { };
    }
    #endregion
}
