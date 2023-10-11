using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Leyer.Models.ViewModels.Category;

public class CreateCategoryVm
{
    [Required()]
    public string CategoryName { get; set; }
    public string? Description { get; set; }
}
