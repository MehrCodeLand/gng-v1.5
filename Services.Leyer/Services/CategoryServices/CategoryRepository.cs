using Azure;
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
using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Category;
using goolrang_sales_v1.Models;

namespace Services.Leyer.Services.CategoryServices;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperDbContext _dbDapper;
    public CategoryRepository(DapperDbContext dapperDb)
    {
        _dbDapper = dapperDb;
    }



    #region Read
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
                Data = categories ,
                Message = $"We found {categories.Count()} categoriy/ies"
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
                Message = "Category is find",
                DataJson = dataJson,
                Data = category
            };
        }
    }

    #endregion

    #region Delete

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

            return new Responses<Category>()
            {
                Message = $"category {id} was deleted.",
            };

        }

    }

    #endregion

    #region Create

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


        return new Responses<Category>()
        {
            Message = $"category : {categoryVm.CategoryName.ToLower()} added",
        };
    }

    #endregion

    #region Update

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

            response.ErrorCode = 1;
            response.Message = $"Category: {categoryVm.Id} updated";

            return response;
        }

        return new Responses<Category>() { };
    }

    #endregion

    #region Validation

    // async is nesecery?
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


        return new Responses<Category>()
        {
            Message = "Done"
        };
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

    
    #endregion


    #region Test Method
    public async Task<Responses<Category>> Test_Method()
    {
        var query = "if exists (select 1 from Category where CategoryId = 4 )" +
            " begin select 100 end ";

        using (var conection = _dbDapper.CreateConnection())
        {
            var result = await conection.QueryAsync<int>(query);

            int i = 100;
        }



        return new Responses<Category>() { };
    }
    #endregion
}
