
using PawsitivelyCare.BLL.Models;
using static PawsitivelyCare.DAL.Entities.Post;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostModel> CreatePost(PostModel PostModel);
        Task<PostModel> GetPost(Guid id);
        Task<List<PostModel>> GetUserPosts(Guid userId);
        Task<List<PostModel>> GetPosts(PostType type, int category, string location);
        Task UpdatePost(PostModel PostModel);
        Task DeletePost(Guid id);
    }
}
