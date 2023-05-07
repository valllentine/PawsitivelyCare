using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DTOs.Post;

namespace PawsitivelyCare.Controllers
{

    [ApiController]
    [Route("api/posts/")]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostsController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPost([FromQuery] Guid id)
        {
            var post = await _postService.GetPost(id);
            return Ok(post);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePost(CreatePostDto postDto)
        {
            var postModel = _mapper.Map<PostModel>(postDto);
            var createdPost = await _postService.CreatePost(postModel);
            return Ok(new { message = "Creation successful", createdPost });
        }

        [HttpPut("update/{id}")]
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
