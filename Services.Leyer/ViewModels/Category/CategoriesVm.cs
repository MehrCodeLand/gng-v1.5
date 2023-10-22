using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.Category;

public class CategoriesVm
{
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string Name { get; set; }
    public string? Description { get; set; }
}
