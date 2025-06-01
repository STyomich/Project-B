using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CommentsService
{
    public class GetCommentByPostId
    {
        public class Query : IRequest<Result<List<CommentDto>>>
        {
            public Guid PostId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<CommentDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<CommentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var comments = await _context.Comments
                    .Where(c => c.PostId == request.PostId)
                    .ToListAsync(cancellationToken);

                if (comments == null || !comments.Any())
                {
                    return Result<List<CommentDto>>.Failure("No comments found for this post");
                }

                return Result<List<CommentDto>>.Success(_mapper.Map<List<CommentDto>>(comments));
            }
        }
    }
}