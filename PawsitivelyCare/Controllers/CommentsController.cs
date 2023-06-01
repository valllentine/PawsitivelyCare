using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DTOs.Comment;
using System.Security.Claims;

namespace PawsitivelyCare.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/comments/")]
    public class CommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public CommentsController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult> GetComments(Guid postId)
        {
            var comments = await _commentService.GetComments(postId);
            return Ok(comments);
        }


        [HttpPost]
        public async Task<ActionResult> CreateComment(CreateCommentDto commentDto)
        {
            var commentModel = _mapper.Map<CommentModel>(commentDto);
            commentModel.SenderId = UserId;
            var createdcomment = await _commentService.CreateComment(commentModel);

            return Ok(new { message = "Creation successful", createdcomment });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(Guid id)
        {
            await _commentService.DeleteComment(id);
            return NoContent();
        }
    }
}
