using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class CommentService : ICommentService
    {
        private readonly IBaseRepository<Comment, Guid> _commentRepository;
        protected readonly IMapper _mapper;

        public CommentService(IBaseRepository<Comment, Guid> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public Task<CommentModel> CreateComment(CommentModel commentModel)
        {
            throw new NotImplementedException();
        }

        public Task<CommentModel> GetComment(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentModel>> GetComments(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComment(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
