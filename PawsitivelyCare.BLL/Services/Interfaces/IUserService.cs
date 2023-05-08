using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Register(UserModel user);
        Task<UserModel> AuthenticateUser(UserModel userModel);
        Task<UserModel> Get(Guid id);
        Task<UserModel> GetByEmail(string email);
        Task Update(UserModel userModel);
        Task Delete(Guid id);
        Task<string> GenerateJwtToken(UserModel userModel);
    }
}
