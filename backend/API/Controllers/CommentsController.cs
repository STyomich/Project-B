using Core.DTOs.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommentsController : BaseApiController
    {
        [HttpGet("{postId}")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByPostId(Guid postId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.CommentsService.GetCommentByPostId.Query { PostId = postId }));
        }

        [HttpPost("{postId}")]
        public async Task<ActionResult<CommentDto>> CreateComment(CommentDto commentDto)
        {
            return HandleResult(await Mediator.Send(new Application.Services.CommentsService.CreateComment.Command { CommentDto = commentDto, User = User }));
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult<Unit>> DeleteComment(Guid commentId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.CommentsService.DeleteComment.Command { Id = commentId }));
        }
        [HttpPost("{postId}/react")]
        public async Task<ActionResult<Unit>> CreateUserReaction(Guid postId, Guid userId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.UserReactionsService.CreateUserReaction.Command { PostId = postId, User = User }));
        }
    }
}