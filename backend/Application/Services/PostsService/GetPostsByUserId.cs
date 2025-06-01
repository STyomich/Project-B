using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.PostsService
{
    public class GetPostsByUserId
    {
        public class Query : IRequest<Result<List<PostDto>>>
        {
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<PostDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<PostDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts
    .Where(p => p.UserId == request.UserId)
    .ToListAsync(cancellationToken);

                // Get reaction counts for the filtered posts
                var postIds = posts.Select(p => p.Id).ToList();

                var reactionCounts = await _context.UserReactions
                    .Where(r => postIds.Contains(r.PostId))
                    .GroupBy(r => r.PostId)
                    .Select(g => new { PostId = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.PostId, x => x.Count, cancellationToken);

                // Map posts to DTOs
                var postsDto = _mapper.Map<List<PostDto>>(posts);

                // Set reaction counts
                foreach (var postDto in postsDto)
                {
                    postDto.ReactionsCount = reactionCounts.TryGetValue(postDto.Id, out var count) ? count : 0;
                }

                return Result<List<PostDto>>.Success(postsDto);

            }
        }
    }
}