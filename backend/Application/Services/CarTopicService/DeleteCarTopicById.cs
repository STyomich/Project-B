using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CarTopicService
{
    public class DeleteCarTopicById
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid CarTopicId { get; set; }
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
                var carTopic = await _context.CarTopics.FindAsync(request.CarTopicId);
                if (carTopic == null) return Result<Unit>.Failure("Car topic not found");
                _context.CarTopics.Remove(carTopic);
                var success = await _context.SaveChangesAsync() > 0;
                if (!success) return Result<Unit>.Failure("Failed to delete car topic");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}