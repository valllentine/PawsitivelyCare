using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Register(UserModel user);
        Task<UserModel> Get(Guid id);
        Task Update(UserModel user);
        Task Delete(Guid id);
    }
}
