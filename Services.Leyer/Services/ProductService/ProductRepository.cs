using Data.Leyer.DbContext;
using Data.Leyer.Models.Structs;
using goolrang_sales_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.Services.ProductService;

public class ProductRepository
{
    private readonly DapperDbContext _dapperDB;

    public ProductRepository(DapperDbContext dapperDb)
    {
        _dapperDB = dapperDb;
    }


    public async Task<Responses<Product>> CreateProduct()
    {





        return new Responses<Product>()
        {
            Message = "created"
        };
    }
}
