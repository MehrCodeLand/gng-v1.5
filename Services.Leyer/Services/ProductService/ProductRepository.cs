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
    public async Task<Responses<Product>> GEtProductById(int id)
    {
        var query = $"select * from Product where ProductId = {id}";

        using(var connectin = _dapperDB.CreateConnection())
        {
            var result = await connectin.QueryAsync<Product>(query);

            if(result.Count() == 1)
            {
                return new Responses<Product>()
                {
                    Data = result,
                    Message = "Done"
                };
            }


            return new Responses<Product>()
            {
                ErrorCode = -100,
                ErrorMessage = "Product was not found"
            };

        }

    }

    #endregion

    #region Create

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

    #endregion

    #region Delete

    public async Task<Responses<Product>> DeleteProductByID( int productId) 
    {
        var query = "delete_product_proc " +
            $" @productID = {productId} ";

        using (var connection = _dapperDB.CreateConnection() )
        {
            var result = await connection.QueryAsync(query);

            if(result.Any(x => x.ErrorMessage != null))
            {
                return new Responses<Product>()
                {
                    ErrorCode = result.First().ErrorCode,
                    ErrorMessage = result.First().ErrorMessage,
                };
            }


            return new Responses<Product>()
            {
                Message = $"product{productId}:delete "
            };
        }
    }

    #endregion

    #region Update 
    public async Task<Responses<Product>> UpdateProduct(UpdateProductVm updateVm )
    {
        var query = $"update_product_proc " +
            $" @productId = {updateVm.ProductId} , " +
            $" @name = '{updateVm.Name}' , " +
            $" @categoryID = {updateVm.CategoryID} ," +
            $" @Description = '{updateVm.Description}' , " +
            $" @unitPrice = {updateVm.UnitPrice} , " +
            $" @QuantityInStock = {updateVm.QuantityInStock}";


        using(var caonnection = _dapperDB.CreateConnection())
        {
            var result = await caonnection.QueryAsync(query);

            if(result.Any(x => x.ErrorMessage != null))
            {
                return new Responses<Product>()
                {
                    ErrorCode = result.First().ErrorCode,
                    ErrorMessage = result.First().ErrorMessage,
                };
            }

            return new Responses<Product>()
            {
                Message = $"product {updateVm.Name} deleted ",
            };
        }
    }



    #endregion
}
