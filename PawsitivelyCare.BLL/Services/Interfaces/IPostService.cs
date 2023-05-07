
using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostModel> CreatePost(PostModel PostModel);
        Task<PostModel> GetPost(Guid id);
        Task UpdatePost(PostModel PostModel);
        Task DeletePost(Guid id);
    }
}
