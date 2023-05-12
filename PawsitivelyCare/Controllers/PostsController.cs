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

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetPost(Guid id)
        //{
        //    var post = await _postService.GetPost(id);
        //    return Ok(post);
        //}

        [HttpGet("myPosts")]
        public async Task<ActionResult<List<PostModel>>> GetUserPosts()
        {
            var posts = await _postService.GetUserPosts(UserId);
            return Ok(posts);
        }

        [HttpGet("")]
        public async Task<ActionResult> GetPosts([FromQuery] PostType type, [FromQuery] int category, [FromQuery] string? location = null)
        {
            var posts = await _postService.GetPosts(type, category, location, UserId);
            return Ok(posts);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePost(CreatePostDto postDto)
        {
            var postModel = _mapper.Map<PostModel>(postDto);
            postModel.CreatorId = UserId;
            var createdPost = await _postService.CreatePost(postModel);

            return Ok(new { message = "Creation successful", createdPost });
        }

        [HttpPost("edit/{id}")]
        public async Task<ActionResult<int>> UpdatePost(Guid id, [FromBody] UpdatePostDto postDto)
        {
            var postModel = await _postService.GetPost(id);

            if (postModel == null)
                return NotFound();

            _mapper.Map(postDto, postModel);
            await _postService.UpdatePost(postModel);

            return Ok(new { message = "Post updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }
    }
}
