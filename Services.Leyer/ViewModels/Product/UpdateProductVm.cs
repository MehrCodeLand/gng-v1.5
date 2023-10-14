using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.Product;

public class UpdateProductVm
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string Name { get; set; }
    [Required]
    public int CategoryID { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}
