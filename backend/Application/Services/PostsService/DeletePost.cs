using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.PostsService
{
    public class DeletePost
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = _context.Posts.FirstOrDefault(p => p.Id == request.Id);
                if (post == null)
                    return Result<Unit>.Failure("Post not found");

                _context.Posts.Remove(post);

                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                    return Result<Unit>.Failure("Failed to delete post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}