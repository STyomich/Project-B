using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CommentsService
{
    public class DeleteComment
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
                var comment = await _context.Comments.FindAsync(request.Id);
                if (comment == null) return Result<Unit>.Failure("Comment not found");

                _context.Comments.Remove(comment);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to delete comment");
            }
        }
    }
}