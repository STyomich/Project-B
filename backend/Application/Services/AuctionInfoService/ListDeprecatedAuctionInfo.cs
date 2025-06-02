using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class ListDeprecatedAuctionInfo
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
                var auctions = await _context.AuctionInfos.Where(a => a.IsActive == false && a.StartDate <= DateTime.UtcNow && a.EndDate <= DateTime.UtcNow)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarImages)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.RegistrationPlate)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarTopic)
                    .ToListAsync(cancellationToken);
                if (auctions == null) return Result<List<AuctionInfoListItemDto>>.Failure("No auctions found");
                return Result<List<AuctionInfoListItemDto>>.Success(_mapper.Map<List<AuctionInfoListItemDto>>(auctions));
            }
        }
    }
}