using goolrang_sales_v1.Models;
using Services.Leyer.Responses.Structs;
using Services.Leyer.ViewModels.User;

namespace Services.Leyer.Services.UserService;

public interface IUserRepository
{
    Task<Responses<User>> CreateUser(CreateUserVm userVm);
    Task<Responses<User>> GetAllUser();
    Task<Responses<User>> DeleteUserById(int userId);
    Task<Responses<User>> UpdateUser(UpdateUserVm userVm);
}