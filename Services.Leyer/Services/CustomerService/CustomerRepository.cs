using Dapper;
using Data.Leyer.DbContext;

using goolrang_sales_v1.Models;
using Services.Leyer.Responses.Structs;
using Services.Leyer.ViewModels.Customer;
using System.Text.RegularExpressions;

namespace Services.Leyer.Services.CustomerService;

public class CustomerRepository : ICustomerRepository
{
    private readonly MyDbContext _dapperDB;
    public CustomerRepository(MyDbContext dapperDb )
    {
        _dapperDB = dapperDb;
    }
    public async Task<Responses<Customer>> CreateCustomer(CreateCustomerVm createCustomerVm)
    {
        var query = $"exec insert_cutomer_proc " +
            $" @FirstName = '{createCustomerVm.FirstName.ToUpper() }' , " +
            $" @LastName = '{createCustomerVm.LastName.ToUpper() }' ," +
            $" @Email = '{createCustomerVm.Email.ToUpper() }', " +
            $" @Phone = '{createCustomerVm.Phone}', " +
            $" @Address = '{createCustomerVm.Address}', " +
            $" @City = '{createCustomerVm.City}', " +
            $" @Sate = '{createCustomerVm.State}', " +
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

        return new Responses<Customer>();
    }
    public async Task<Responses<Customer>> DeleteCustomer(DeleteCustomerVm deleteVm)
    {
        if(deleteVm.CutomerID != -1)
        {
            var query = $"delete_cutomer_byID_proc @customerID = {deleteVm.CutomerID} ";

            using (var connecttion = _dapperDB.CreateConnection())
            {
                var result = await connecttion.QueryAsync<int>(query);

                if(result.Any(x => x == -100))
                {
                    return new Responses<Customer>()
                    {
                        ErrorCode = -100,
                        ErrorMessage = "user not found"
                    };
                }

                return new Responses<Customer>();
            }

        }
        if(deleteVm.Email != null || deleteVm.Email != "string")
        {
            var query = $"delete_customer_byEmai_proc @email = '{deleteVm.Email.ToUpper()}' ";

            using (var connecttion = _dapperDB.CreateConnection())
            {
                var result = await connecttion.QueryAsync<int>(query);

                if (result.Any(x => x == -100))
                {
                    return new Responses<Customer>()
                    {
                        ErrorCode = -100,
                        ErrorMessage = $"user by {deleteVm.Email} email not found"
                    };
                }
                return new Responses<Customer>();
            }
        }

        return new Responses<Customer>()
        {
            ErrorCode = -100,
            ErrorMessage = "Somthings wrong"
        };
    }
    public async Task<Responses<Customer>> GetAllCustomer()
    {
        var query = "select * from Customer ";

        using (var conection = _dapperDB.CreateConnection())
        {
            var customers = await conection.QueryAsync<Customer>(query);

            if(customers.Count() > 0)
            {
                return new Responses<Customer>()
                {
                    Data = customers,
                };
            }

            return new Responses<Customer>()
            {
                ErrorCode = -300,
                ErrorMessage = "we have no customers here"
            };
        }
    }
    public async Task<Responses<Customer>> GetUserById(int id)
    {
        if(id <= 0)
        {
            return new Responses<Customer>()
            {
                ErrorCode = -100,
                ErrorMessage = "invalid id"
            };
        }

        var query = $"select * from Customer where CustomerId = {id}";

        using( var connection  = _dapperDB.CreateConnection())
        {
            var customer = await connection.QueryAsync<Customer>(query);
            if(customer.Count() == 1)
            {
                return new Responses<Customer>()
                {
                    Data = customer,
                };
            }

            return new Responses<Customer>()
            {
                ErrorCode = -200,
                ErrorMessage = "no result"
            };
        }
    } 
    public async Task<Responses<Customer>> UpdateCustomer(UpdateCustomerVm updateVm)
    {
        var query = $"update_customer_proc @FirstName = '{updateVm.Firstname}' , " +
            $"@LastName = '{updateVm.Lastname}' " +
            $"@State = '{updateVm.State}' " +
            $"@City = '{updateVm.City}' " +
            $"@CustomerID = {updateVm.CustomerID}";

        using(var  connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync<int>(query);
            if(result.Any(x => x == -100))
            {
                return new Responses<Customer>()
                {
                    ErrorCode = -100,
                    ErrorMessage = "Somthings Wrong"
                };
            }
            return new Responses<Customer>();
        }
    }
}
