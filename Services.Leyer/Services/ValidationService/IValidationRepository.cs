namespace Services.Leyer.Services.ValidationService
{
    public interface IValidationRepository
    {
        bool EmailValidation(string email);
        public bool PhoneValidate(string phone);
    }
}