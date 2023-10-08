namespace goolrang_sales_v1.Models;

public class Inventory
{
    public int InventoryId { get; set; }
    public int ProductId { get; set; }
    public int QuantityInStock { get; set; }
    public string LastRestockedDate { get; set; }
}
