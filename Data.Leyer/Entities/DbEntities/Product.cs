namespace goolrang_sales_v1.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int CategoryID { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}
