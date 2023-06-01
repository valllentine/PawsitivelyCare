
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
        private readonly IBaseRepository<Image, Guid> _imageRepository;
        protected readonly IMapper _mapper;

        public PostService(IBaseRepository<Post, Guid> postRepository, IBaseRepository<Image, Guid> imageRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _imageRepository = imageRepository;
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

        public async Task<List<PostModel>> GetPosts(PostType type, int? category, string? location, Guid userId)
        {
            return _mapper.Map<List<PostModel>>(await _postRepository.Query(
                p => p.Type == type &&
                (category == 0 || (p.PostCategoryId == category)) &&
                (location == null || (p.Location == location)) &&
                (p.CreatorId != userId),
                orderBy: p => p.OrderByDescending(d => d.CreatedAt)));
        }

        public async Task<List<string>> GetPostImages(Guid postId)
        {
            var postImages = _mapper.Map<List<ImageModel>>(await _imageRepository.Query(i => i.PostId == postId));

            var imagePaths = new List<string>();
            foreach (var image in postImages)
            {
                imagePaths.Add(image.FileName);
            }

            return imagePaths;
        }

        public async Task UploadImages(List<string> images, Guid postId)
        {
            foreach (var image in images)
            {
                var imageEntity = new Image
                {
                    FileName = image,
                    PostId = postId
                };

                await _imageRepository.AddAsync(imageEntity);
            }
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
