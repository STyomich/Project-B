using Application.Helpers;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class CreateAuctionInfo
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AuctionInfoCreateRequest? AuctionInfo { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var auctionInfo = _mapper.Map<AuctionInfo>(request.AuctionInfo);
                var existingAuction = await _context.AuctionInfos
                    .FirstOrDefaultAsync(x => x.CarId == auctionInfo.CarId, cancellationToken);
                if (existingAuction != null)
                {
                    return Result<Unit>.Failure("Main auction already exists");
                }
                _context.AuctionInfos.Add(auctionInfo);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!success) return Result<Unit>.Failure("Failed to create auction");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}