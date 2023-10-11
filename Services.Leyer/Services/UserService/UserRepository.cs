using Azure;
using Dapper;
using Data.Leyer.DbContext;
using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.User;
using goolrang_sales_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Leyer.Services.UserService;

public class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dapperDB;
    public UserRepository(DapperDbContext dapperDb)
    {
        _dapperDB = dapperDb;
    }




    #region Read

    public async Task<Responses<User>> GetAllUser()
    {
        var query = "select *from [User]";


        using(var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync<User>(query);
            if(result.Count() > 0)
            {
                return new Responses<User>()
                {
                    Data = result,
                    Message = "OK"
                };
            }


            return new Responses<User>()
            {
                ErrorCode = -200,
                Message = "we have no User yet"
            };
        }




    }

    #endregion
    public async Task<Responses<User>> CreateUser(CreateUserVm userVm)
    {
        var query = $"insert_user_proc " +
            $"@firstName = '{userVm.FirstName}' ," +
            $"@lastName = '{userVm.LastName}' , " +
            $"@email = '{userVm.Email.ToUpper()}' , " +
            $"@phone = '{userVm.Phone}' ";


        using (var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Any(x => x.ErrorCode == null))
            {
                return new Responses<User>()
                {
                    Message = $"user {userVm.FirstName} created",
                };
            }


            return new Responses<User>()
            {
                ErrorCode = result.First().ErrorCode,
                ErrorMessage = result.First().Message,
            };
        }
    }
}
