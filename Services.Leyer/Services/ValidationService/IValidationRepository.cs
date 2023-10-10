using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Customer;
using goolrang_sales_v1.Models;

namespace Services.Leyer.Services.ValidationService
{
    public interface IValidationRepository
    {
        bool EmailValidation(string email);
        bool PhoneValidate(string phone);
    }
}