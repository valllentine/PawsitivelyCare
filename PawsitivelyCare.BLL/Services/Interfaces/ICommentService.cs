using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentModel> CreateComment(CommentModel commentModel);
        Task<List<CommentUserModel>> GetComments(Guid postId);
        Task DeleteComment(Guid id);
    }
}
