using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Leyer.Models.ViewModels.Category;

public class UpdateCategoryVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
