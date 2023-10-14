using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.BaseVm;

public class EntityBaseVm
{
    [Required]
    public int Id { get; init; }
    [Required]
    [MinLength(2 , ErrorMessage = "MinLength:2")]
    [MaxLength(100 , ErrorMessage = "MaxLength:100")]
    public string Name { get; init; }
    [EmailAddress]
    [Required]
    [MinLength(5)]
    public string Email { get; set; }
}
