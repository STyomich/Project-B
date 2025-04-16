using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.AuctionInfoService
{
    public class DeleteAuctionInfo
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
                var auction = await _context.AuctionInfos.FindAsync(request.Id);
                if (auction == null) return Result<Unit>.Failure("Auction not found");

                _context.AuctionInfos.Remove(auction);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!success) return Result<Unit>.Failure("Failed to delete auction");

                return Result<Unit>.Success(Unit.Value);
            }
        }   
    }
}