using Dapper;
using Data.Leyer.DbContext;
using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Product;
using goolrang_sales_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.Services.ProductService;

public class ProductRepository : IProductRepository
{
    private readonly DapperDbContext _dapperDB;

    public ProductRepository(DapperDbContext dapperDb)
    {
        _dapperDB = dapperDb;
    }

    #region Read

    public async Task<Responses<Product>> GetAllProduct()
    {
        var query = "select * from Product";


        using(var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync<Product>(query);

            if(result.Count() > 0 )
            {
                return new Responses<Product>()
                {
                    Data = result,
                    Message = $"we found {result.Count()} products "
                };
            }


            return new Responses<Product>()
            {
                ErrorCode = -100,
                ErrorMessage = "We have no product"
            };
        }




    }

    #endregion
    public async Task<Responses<Product>> CreateProduct(CreateProductVm productVm)
    {
        var query = $"exec insert_product_proc " +
            $"@categoryID = {productVm.CategoryID} ," +
            $"@Description = '{productVm.Description}', " +
            $"@name = '{productVm.Name}' ," +
            $"@unitPrice = {productVm.UnitPrice} ," +
            $"@QuantityInStock = {productVm.QuantityInStock}  ";


        using (var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if (result.Any(x => x.ErrorMessage != null))
            {

                return new Responses<Product>()
                {
                    ErrorCode = result.First().ErrorCode,
                    ErrorMessage = result.First().ErrorMessage,
                };
            }

            return new Responses<Product>()
            {
                Message = $"{productVm.Name} : inserted"
            };
        }
    }
}
