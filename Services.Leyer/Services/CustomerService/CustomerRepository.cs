using Dapper;
using Data.Leyer.DbContext;
using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Category;
using Data.Leyer.Models.ViewModels.Customer;
using goolrang_sales_v1.Models;
using Services.Leyer.Services.ValidationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Leyer.Services.CustomerService;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperDbContext _dapperDB;
    private readonly IValidationRepository _validate;
    public CustomerRepository(DapperDbContext dapperDb , IValidationRepository validation )
    {
        _validate = validation;
        _dapperDB = dapperDb;
    }


    #region Create

    #endregion
    public async Task<Responses<Customer>> CreateCustomer(CreateCustomerVm createCustomerVm)
    {
        var response = await ValidateCreateCustomerVm(createCustomerVm);
        if(response.ErrorCode < 0 )
            return response;



        var query = $"exec insert_cutomer_proc " +
            $" @FirstName = '{createCustomerVm.FirstName.ToUpper() }' , " +
            $" @LastName = '{createCustomerVm.LastName.ToUpper() }' " +
            $" @Email = '{createCustomerVm.Email.ToUpper() }' " +
            $" @Phone = '{createCustomerVm.Phone}' " +
            $" @Address = '{createCustomerVm.Address}' " +
            $" @City = '{createCustomerVm.City}' " +
            $" @State = '{createCustomerVm.State}' " +
            $" @ZipCode = '{createCustomerVm.ZipCode}' ";

        using (var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync<int>(query);
            if(result.Any(x => x == -100))
            {
                return new Responses<Customer>()
                {
                    ErrorCode = -200,
                    ErrorMessage = "email pr phon number is exist"
                };
            }
        }

        return new Responses<Customer>()
            {
                Message = $"customer:{createCustomerVm.FirstName} added",
            };
    }



    #region Preavte

    private async Task<Responses<Customer>> ValidateCreateCustomerVm(CreateCustomerVm createCustomerVm)
    {
        if (createCustomerVm.FirstName == null || createCustomerVm.FirstName.Length <= 2)
        {
            return new Responses<Customer>()
            {
                ErrorCode = -150,
                ErrorMessage = "Name is to short!",
            };
        }
        else if (createCustomerVm.FirstName.Length > 25)
        {
            return new Responses<Customer>()
            {
                ErrorCode = -160,
                ErrorMessage = "Name is to long",
            };
        }
        else if (HasNumber(createCustomerVm.FirstName))
        {
            return new Responses<Customer>()
            {
                ErrorCode = -200,
                ErrorMessage = "name without number",
            };
        }
        else if (createCustomerVm.LastName.Length < 2 || createCustomerVm.LastName == null)
        {
            return new Responses<Customer>()
            {
                ErrorCode = -210,
                ErrorMessage = "lastname is to short",
            };
        }
        else if (createCustomerVm.LastName.Length > 20)
        {
            return new Responses<Customer>()
            {
                ErrorCode = -225,
                ErrorMessage = "lastname is to long",
            };
        }
        else if (( _validate.EmailValidation(createCustomerVm.Email) == false))
        {
            return new Responses<Customer>()
            {
                ErrorCode = -300,
                ErrorMessage = "email has incorect format",
            };
        }
        else if( createCustomerVm.Address == null || createCustomerVm.Address.Length < 5)
        {
            return new Responses<Customer>()
            {
                ErrorCode = -210,
                ErrorMessage = "address should complated",
            };
        }
        else if (_validate.PhoneValidate(createCustomerVm.Phone))
        {
            return new Responses<Customer>()
            {
                ErrorCode = -230,
                ErrorMessage = "phone number has incorect type",
            };
        }
        return new Responses<Customer>()
        {
            Message = "OK",
        };
    }



    private bool HasNumber(string name)
    {
        var reg = new Regex("[0-9]");

        return reg.Match(name).Success;

    }
    #endregion
}
