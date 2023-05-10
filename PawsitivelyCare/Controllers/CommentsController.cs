using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> Getcomment(Guid id)
        {
            var comment = await _commentService.GetComment(id);
            return Ok(comment);
        }

        //[HttpGet("{postId}")]
        //public async Task<ActionResult<List<CommentModel>>> GetComments(Guid postId)
        //{
        //    var comments = await _commentService.GeComments(postId);
        //    return Ok(comments);
        //}


        //[HttpPost("create")]
        //public async Task<ActionResult> Createcomment(CreateCommentDto commentDto)
        //{
        //    var commentModel = _mapper.Map<CommentModel>(commentDto);
        //    commentModel.CreatorId = UserId;
        //    var createdcomment = await _commentService.Createcomment(commentModel);

        //    return Ok(new { message = "Creation successful", createdcomment });
        //}

        //[Httpcomment("edit/{id}")]
        //public async Task<ActionResult<int>> Updatecomment(Guid id, [FromBody] UpdatecommentDto commentDto)
        //{
        //    var commentModel = await _commentService.Getcomment(id);

        //    if (commentModel == null)
        //        return NotFound();

        //    _mapper.Map(commentDto, commentModel);
        //    await _commentService.Updatecomment(commentModel);

        //    return Ok(new { message = "comment updated successfully" });
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Deletecomment(Guid id)
        //{
        //    await _commentService.Deletecomment(id);
        //    return NoContent();
        //}
    }
}
