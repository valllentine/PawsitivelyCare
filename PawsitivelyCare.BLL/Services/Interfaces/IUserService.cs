using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Register(UserModel user);
        Task<string> Login(UserModel userModel);
        Task<UserModel> Get(Guid id);
        Task Update(UserModel userModel);
        Task Delete(Guid id);
    }
}
