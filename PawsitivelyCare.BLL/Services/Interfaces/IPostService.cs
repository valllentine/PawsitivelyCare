
using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostModel> CreatePost(PostModel PostModel);
        Task<PostModel> GetPost(Guid id);
        Task<List<PostModel>> GetPostsList(Guid userId);
        Task UpdatePost(PostModel PostModel);
        Task DeletePost(Guid id);
    }
}
