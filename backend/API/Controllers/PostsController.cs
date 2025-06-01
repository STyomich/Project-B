using Core.DTOs.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetAllPosts()
        {
            return HandleResult(await Mediator.Send(new Application.Services.PostsService.GetAllPosts.Query()));
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostInfo>> GetPostById(Guid postId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.PostsService.GetPostInformationById.Query { PostId = postId }));
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<PostDto>>> GetPostsByUserId(Guid userId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.PostsService.GetPostsByUserId.Query { UserId = userId }));
        }
        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByPostId(Guid postId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.CommentsService.GetCommentByPostId.Query { PostId = postId }));
        }
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost(PostDto postDto)
        {
            return HandleResult(await Mediator.Send(new Application.Services.PostsService.CreatePost.Command { PostDto = postDto, User = User }));
        }
        [HttpDelete("{postId}")]
        public async Task<ActionResult<Unit>> DeletePost(Guid postId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.PostsService.DeletePost.Command { Id = postId }));
        }
        [HttpPost("{postId}/react")]
        public async Task<ActionResult<Unit>> CreateUserReaction(Guid postId, UserReactionDto userReactionDto)
        {
            return HandleResult(await Mediator.Send(new Application.Services.UserReactionsService.CreateUserReaction.Command { PostId = postId, User = User }));
        }
    }
}