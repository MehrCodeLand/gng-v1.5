namespace goolrang_sales_v1.Models;

public class InvoiceItem
{
    public int InvoiceItemId { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public decimal UnitPrice { get; set; }

}
