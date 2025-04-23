using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class GetAuctionInfoById
    {
        public class Query : IRequest<Result<AuctionInfoDto>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<AuctionInfoDto>>
        {
            private readonly DataContext _context;
            private readonly IAuctionInfoService _auctionInfoService;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IAuctionInfoService auctionInfoService, IMapper mapper)
            {
                _mapper = mapper;
                _auctionInfoService = auctionInfoService;
                _context = context;
            }

            public async Task<Result<AuctionInfoDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var auctionInfo = await _context.AuctionInfos
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarImages)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.RegistrationPlate)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarTopic)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.CarDocuments)
                    .Include(a => a.Car!)
                        .ThenInclude(c => c.User)
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                var maxBid = await _context.AuctionBids
                    .Where(b => b.AuctionInfoId == request.Id)
                    .OrderByDescending(b => b.BidAmount)
                    .FirstOrDefaultAsync(cancellationToken);

                var auctionInfoDto = _mapper.Map<AuctionInfoDto>(auctionInfo);
                auctionInfoDto.MaxBid = _mapper.Map<AuctionBidDto>(maxBid);

                if (auctionInfo == null) return Result<AuctionInfoDto>.Failure("Auction info is null");

                return Result<AuctionInfoDto>.Success(auctionInfoDto);
            }
        }
    }
}