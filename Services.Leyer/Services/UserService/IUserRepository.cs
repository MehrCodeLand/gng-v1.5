using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.User;
using goolrang_sales_v1.Models;

namespace Services.Leyer.Services.UserService
{
    public interface IUserRepository
    {
        Task<Responses<User>> CreateUser(CreateUserVm userVm);
        Task<Responses<User>> GetAllUser();
        Task<Responses<User>> DeleteUserById(int userId);
        Task<Responses<User>> UpdateUser(UpdateUserVm userVm);
    }
}