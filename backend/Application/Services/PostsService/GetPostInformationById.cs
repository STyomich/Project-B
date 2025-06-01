using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.PostsService
{
    public class GetPostInformationById
    {
        public class Query : IRequest<Result<PostInfo>>
        {
            public Guid PostId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<PostInfo>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PostInfo>> Handle(Query request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts
                    .Include(p => p.User)
                    .Include(p => p.UserReactions)
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

                if (post == null)
                {
                    return Result<PostInfo>.Failure("Post not found");
                }

                // Map the post to PostInfo DTO
                var postInfo = _mapper.Map<PostInfo>(post);

                // Set the reactions count
                postInfo.ReactionsCount = post.UserReactions?.Count ?? 0;

                return Result<PostInfo>.Success(postInfo);
            }
        }
    }
}