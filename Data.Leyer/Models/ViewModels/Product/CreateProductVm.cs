using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Leyer.Models.ViewModels.Product;

public class CreateProductVm
{
    public string Name { get; set; }
    public int CategoryID { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}
