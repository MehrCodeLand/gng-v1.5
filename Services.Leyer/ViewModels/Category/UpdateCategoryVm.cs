using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.ViewModels.Category;

public class UpdateCategoryVm
{
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
}
