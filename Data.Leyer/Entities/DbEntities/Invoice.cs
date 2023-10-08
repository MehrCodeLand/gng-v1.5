namespace goolrang_sales_v1.Models;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    public int InvoiceDate { get; set; }
    public int UserId { get; set; }
    public decimal? TotalAmount { get; set; }
}
