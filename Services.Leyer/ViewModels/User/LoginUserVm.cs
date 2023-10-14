using Services.Leyer.ViewModels.BaseVm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.ViewModels.User;

public class LoginUserVm : EntityBaseVm
{
    [DataType(DataType.Password)]
    public string Password { get; set; }    
}
