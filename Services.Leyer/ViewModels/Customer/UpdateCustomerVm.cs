using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.Customer;

public class UpdateCustomerVm
{
    public int CustomerID { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}
