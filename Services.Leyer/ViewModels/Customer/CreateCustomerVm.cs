using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.Customer;

public class CreateCustomerVm
{
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string LastName { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string? ZipCode { get; set; }

}
