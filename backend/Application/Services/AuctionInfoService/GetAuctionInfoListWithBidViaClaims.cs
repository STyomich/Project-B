using System.Security.Claims;
using Application.Helpers;
using Application.Repositories;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class GetAuctionInfoListWithBidViaClaims
    {
        public class Query : IRequest<Result<List<AuctionInfoListItemDto>>>
        {
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<AuctionInfoListItemDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserRepository _userRepository;
            public Handler(DataContext context, IMapper mapper, UserRepository userRepository)
            {
                _context = context;
                _mapper = mapper;
                _userRepository = userRepository;
            }
            public async Task<Result<List<AuctionInfoListItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.User == null)
                    return Result<List<AuctionInfoListItemDto>>.Failure("User cannot be null");
                var email = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                    return Result<List<AuctionInfoListItemDto>>.Failure("User email cannot be null");
                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<List<AuctionInfoListItemDto>>.Failure("User not found");

                var auctions = await _context.AuctionInfos
                    .Where(ai => ai.AuctionBids != null && ai.AuctionBids.Any(ab => ab.UserId == user.Id))
                    .Include(ai => ai.Car!)
                    .ThenInclude(c => c.CarImages)
                    .Include(ai => ai.Car!)
                    .ThenInclude(c => c.RegistrationPlate)
                    .Include(ai => ai.Car!)
                    .ThenInclude(c => c.CarTopic)
                    .ToListAsync();
                return Result<List<AuctionInfoListItemDto>>.Success(_mapper.Map<List<AuctionInfoListItemDto>>(auctions));
            }
        }
    }
}