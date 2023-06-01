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
        private readonly IBaseRepository<Post, Guid> _postRepository;
        private readonly IBaseRepository<User, Guid> _userRepository;
        protected readonly IMapper _mapper;

        public CommentService(IBaseRepository<Comment, Guid> commentRepository, IBaseRepository<Post, Guid> postRepository,
            IBaseRepository<User, Guid> userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CommentModel> CreateComment(CommentModel commentModel)
        {
            var commentEntity = _mapper.Map<Comment>(commentModel);
            commentEntity.CreatedAt = DateTime.Now;
            var createdComment = await _commentRepository.AddAsync(commentEntity);

            return _mapper.Map<CommentModel>(createdComment);
        }

        public async Task<List<CommentUserModel>> GetComments(Guid postId)
        {
            var comments = await _commentRepository.Query(p => p.PostId == postId);
            var commentsModels = _mapper.Map<List<CommentUserModel>>(comments);

            foreach ( var comment in commentsModels) 
            { 
                var creator = await _userRepository.QueryFirst(p => p.Id == comment.Comment.SenderId);
                comment.Creator = _mapper.Map<UserModel>(creator);
            }

            return commentsModels;
        }

        public async Task DeleteComment(Guid id)
        {
            var commentEntity = await _commentRepository.GetAsync(id);

            if (commentEntity == null)
                throw new ArgumentException($"Chosen comment could not be found.");

            await _commentRepository.DeleteAsync(commentEntity);
        }
    }
}
