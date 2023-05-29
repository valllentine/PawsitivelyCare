using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DTOs.Post;
using System.Security.Claims;
using static PawsitivelyCare.DAL.Entities.Post;

namespace PawsitivelyCare.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/posts/")]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        private Guid UserId => Guid.Parse(User.Claims.Single(c=>c.Type == ClaimTypes.NameIdentifier).Value);

        public PostsController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPosts([FromQuery] PostType type, [FromQuery] int category, [FromQuery] string? location = null)
        {
            var posts = await _postService.GetPosts(type, category, location, UserId);
            return Ok(posts);
        }

        [HttpGet("myPosts")]
        public async Task<ActionResult<List<PostModel>>> GetUserPosts()
        {
            var posts = await _postService.GetUserPosts(UserId);
            return Ok(posts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetPost(Guid id)
        {
            var post = await _postService.GetPost(id);
            return Ok(post);
        }

        [HttpGet("{postId:guid}/images")]
        public async Task<ActionResult> GetPostImages(Guid postId)
        {
            var imagePaths = await _postService.GetPostImages(postId);

            if (imagePaths.Count == 0)
            {
                return Ok(imagePaths);
            }

            return Ok(imagePaths);
        }

        [HttpPost("{postId:guid}/images")]
        public async Task<ActionResult> UploadImages(List<string> images, Guid postId)
        {
            if (images == null || images.Count == 0) 
            { 
                return BadRequest("Could not upload images"); 
            }

            await _postService.UploadImages(images, postId);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdatePost(Guid id, [FromBody] UpdatePostDto postDto)
        {
            var postModel = await _postService.GetPost(id);

            if (postModel == null)
                return NotFound();

            _mapper.Map(postDto, postModel);
            await _postService.UpdatePost(postModel);

            return Ok(new { message = "Post updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostDto postDto)
        {
            var postModel = _mapper.Map<PostModel>(postDto);
            postModel.CreatorId = UserId;
            var createdPost = await _postService.CreatePost(postModel);

            return Ok(createdPost);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }
    }
}
