
using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using static PawsitivelyCare.DAL.Entities.Post;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class PostService : IPostService
    {
        private readonly IBaseRepository<Post, Guid> _postRepository;
        protected readonly IMapper _mapper;

        public PostService(IBaseRepository<Post, Guid> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostModel> CreatePost(PostModel postModel)
        {
            var postEntity = _mapper.Map<Post>(postModel);
            postEntity.CreatedAt = DateTime.Now;
            var createdPost = await _postRepository.AddAsync(postEntity);

            return _mapper.Map<PostModel>(createdPost);
        }

        public async Task<PostModel> GetPost(Guid id)
        {
            var User = await _postRepository.GetAsync(id);

            return _mapper.Map<PostModel>(User);
        }

        public async Task<List<PostModel>> GetUserPosts(Guid userId)
        {
            return _mapper.Map<List<PostModel>>(await _postRepository.Query(
                p => p.CreatorId == userId,
                orderBy: p=>p.OrderByDescending(d=>d.CreatedAt)));
        }

        public async Task<List<PostModel>> GetPosts(PostType type, int category, string location)
        {
            category = default;
            location = default;
            return _mapper.Map<List<PostModel>>(await _postRepository.Query(
                p => p.Type == type &&
                p.PostCategoryId == category &&
                p.Location == location,
                orderBy: p => p.OrderByDescending(d => d.CreatedAt)));
        }


        public async Task UpdatePost(PostModel postModel)
        {
            var postEntity = _mapper.Map<Post>(postModel);

            await _postRepository.UpdateAsync(postEntity);
        }

        public async Task DeletePost(Guid id)
        {
            var postEntity = await _postRepository.GetAsync(id);

            if (postEntity == null)
                throw new ArgumentException($"User with the id '{id}' could not be found.");

            await _postRepository.DeleteAsync(postEntity);
        }
    }
}
