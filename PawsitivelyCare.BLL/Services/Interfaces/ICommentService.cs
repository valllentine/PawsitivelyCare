using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentModel> CreateComment(CommentModel commentModel);
        Task<CommentModel> GetComment(Guid id);
        Task<List<CommentModel>> GetComments(Guid postId);
        Task DeleteComment(Guid id);
    }
}
