using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class ListUpcomingAuctionInfo
    {
        public class Query : IRequest<Result<List<AuctionInfoListItemDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<AuctionInfoListItemDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<AuctionInfoListItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var auctions = await _context.AuctionInfos
                    .Where(a => a.IsActive && a.StartDate > DateTime.UtcNow)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarImages)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.RegistrationPlate)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarTopic)
                    .ToListAsync(cancellationToken);

                // Get all auction IDs
                var auctionIds = auctions.Select(a => a.Id).ToList();

                // Get max bid per auction (optional optimization)
                var maxBids = await _context.AuctionBids
                    .Where(b => auctionIds.Contains(b.AuctionInfoId))
                    .GroupBy(b => b.AuctionInfoId)
                    .Select(g => g.OrderByDescending(b => b.BidAmount).First())
                    .ToListAsync(cancellationToken);

                // Map auction to DTOs and attach max bids
                var auctionDtos = auctions.Select(auction =>
                {
                    var dto = _mapper.Map<AuctionInfoListItemDto>(auction);
                    var maxBid = maxBids.FirstOrDefault(b => b.AuctionInfoId == auction.Id);
                    dto.MaxBid = _mapper.Map<AuctionBidDto>(maxBid);
                    return dto;
                }).ToList();


                if (auctions == null) return Result<List<AuctionInfoListItemDto>>.Failure("No auctions found");
                return Result<List<AuctionInfoListItemDto>>.Success(_mapper.Map<List<AuctionInfoListItemDto>>(auctionDtos));
            }
        }
    }
}