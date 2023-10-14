using Dapper;
using Data.Leyer.DbContext;
using goolrang_sales_v1.Models;
using Services.Leyer.Responses.Structs;
using Services.Leyer.Security;
using Services.Leyer.ViewModels.User;

namespace Services.Leyer.Services.UserService;
public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dapperDB;
    public UserRepository(MyDbContext dapperDb)
    {
        _dapperDB = dapperDb;
    }
    public async Task<Responses<User>> GetAllUser()
    {
        var query = "select * from [User]";

        using(var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync<User>(query);
            if(result.Count() > 0)
            {
                return new Responses<User>()
                {
                    Data = result,
                };
            }
            return new Responses<User>()
            {
                ErrorCode = -200,
                ErrorMessage = "we have no User yet"
            };
        }
    }
    public async Task<Responses<User>> CreateUser(CreateUserVm userVm)
    {
        if(userVm.RePassword != userVm.Password)
        {
            return new Responses<User>()
            {
                ErrorCode = -300,
                ErrorMessage = "the password and repassword are not match"
            };
        }

        var hashPassword = HashPasswordC.EncodePasswordMd5(userVm.Password);
        var query = $"insert_user_proc " +
            $"@firstName = '{userVm.FirstName}' ," +
            $"@lastName = '{userVm.LastName}' , " +
            $"@email = '{userVm.Email.ToUpper()}' , " +
            $"@phone = '{userVm.Phone}' , " +
            $"@Password = '{hashPassword}' ";

        using (var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Any(x => x.ErrorCode == null))
            {
                return new Responses<User>();
            }

            return new Responses<User>()
            {
                ErrorCode = result.First().ErrorCode,
                ErrorMessage = result.First().Message,
            };
        }
    }
    public async Task<Responses<User>> DeleteUserById(int userId)
    {
        var query = $"delete_user_proc_adv " +
            $" @userId = {userId}";

        using( var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync(query);
            if(result.Any(x => x.ErrorCode == null))
            {

                return new Responses<User>();
            }

            return new Responses<User>()
            {
                ErrorCode = result.First().ErrorCode,
                ErrorMessage = result.First().ErrorMessage,
            };
        }
    }
    public async Task<Responses<User>> UpdateUser(UpdateUserVm userVm)
    {
        var query = $"user_update_proc_adv " +
            $" @UserId = {userVm.UserId} , " +
            $" @firstName = '{userVm.FirstName}' , " +
            $" @lastName= '{userVm.LastName}' ";

        using( var connection = _dapperDB.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Any(x => x.ErrorCode == null))
            {
                return new Responses<User>();
            }

            return new Responses<User>()
            {
                ErrorCode = result.First().ErrorCode,
                ErrorMessage = result.First().ErrorMessage,
            };
        }
    }
}
