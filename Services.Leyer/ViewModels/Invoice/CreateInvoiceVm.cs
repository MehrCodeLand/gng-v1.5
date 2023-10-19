using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.Invoice;

public class CreateInvoiceVm
{
    [Required]
    public int InvoiceId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public string InvoiceDate { get; set; } = DateTime.Now.ToString();
    [Required]
    public int UserId { get; set; }
    public decimal? TotalAmount { get; set; }
}
