using goolrang_sales_v1.Models;
using Services.Leyer.Responses.Structs;
using Services.Leyer.ViewModels.Product;

namespace Services.Leyer.Services.ProductService;

public interface IProductRepository
{
    Task<Responses<Product>> CreateProduct(CreateProductVm productVm);
    Task<Responses<Product>> GetAllProduct();
    Task<Responses<Product>> DeleteProductByID(int productId);
    Task<Responses<Product>> UpdateProduct(UpdateProductVm updateVm);
    Task<Responses<Product>> GEtProductById(int id);
}