using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Add(UserModel user);
        Task<UserModel> Get(int id);
        Task Update(UserModel user);
        Task Delete(int id);
    }
}
