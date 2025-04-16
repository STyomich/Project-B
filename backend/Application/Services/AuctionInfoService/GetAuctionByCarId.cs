using Application.Helpers;
using Core.Domain.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class GetAuctionByCarId
    {
        public class Query : IRequest<Result<AuctionInfo>>
        {
            public Guid CarId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AuctionInfo>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<AuctionInfo>> Handle(Query request, CancellationToken cancellationToken)
            {
                var auction = await _context.AuctionInfos.FirstOrDefaultAsync(x => x.CarId == request.CarId, cancellationToken);
                if (auction == null) return Result<AuctionInfo>.Failure("Auction not found");
                return Result<AuctionInfo>.Success(auction);
            }
        }
    }
}