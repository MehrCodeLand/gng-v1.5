using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Customer;
using Data.Leyer.Models.ViewModels.Product;
using goolrang_sales_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Leyer.Services.ValidationService;

public class ValidationRepository : IValidationRepository
{

    public bool EmailValidation(string email)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (regex.IsMatch(email) && email != null)
            return true;


        return false;
    }
    public bool PhoneValidate(string phone)
    {
        if (phone.Length == 11)
            return false;
        if (!PhoneRegEx(phone))
            return false;


        return true;
    }

    public async Task<Responses<Product>> ValidateCreateProduct( CreateProductVm productVm )
    {
        if(productVm.Name.Length < 3 || productVm.Name.Length > 50)
        {
            return new Responses<Product>()
            {
                ErrorCode = -150,
                ErrorMessage = "please cheack lenght "
            };
        }




        return new Responses<Product>()
        {
            Message = "done"
        };
    }


    private bool PhoneRegEx(string phone)
    {
        var reg = new Regex("[0-9]");

        return reg.IsMatch(phone);
    }
}
