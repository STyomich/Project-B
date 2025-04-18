using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.AuctionBidService
{
    public class CreateAuctionBid
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AuctionBidDto? AuctionBid { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IAuctionBidService _auctionBidService;
            private readonly IMapper _mapper;
            public Handler(IAuctionBidService auctionBidService, IMapper mapper)
            {
                _auctionBidService = auctionBidService;
                _mapper = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.AuctionBid == null) return Result<Unit>.Failure("Invalid auction bid data");

                var auctionBid = _mapper.Map<AuctionBid>(request.AuctionBid);                

                var result = await _auctionBidService.PlaceBid(auctionBid);
                if (!result) return Result<Unit>.Failure("Failed to place bid");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}