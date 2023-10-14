using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.User;

public class LoginUserVm
{
    [Required]
    public int UserID { get; set; }
    [EmailAddress]
    [MinLength(4)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }    
}
