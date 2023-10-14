using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.User;

public class CreateUserVm
{
    [Required(ErrorMessage = "please enter data")]
    [MinLength(2 , ErrorMessage = "min len is 2")]
    [MaxLength(50, ErrorMessage = "max len is 50")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "please enter data")]
    [MinLength(2, ErrorMessage = "min len is 2")]
    [MaxLength(50, ErrorMessage = "max len is 50")]
    public string LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "thats incorect email format")]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }

    [DataType(DataType.Password)]
    [MinLength(5)]
    [MaxLength(15)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare(nameof(Password) , ErrorMessage = "plesae check pass and re-pass")]
    [MinLength(5)]
    public string RePassword { get; set; }
}
