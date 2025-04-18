using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionInfoService
{
    public class AuctionInfoService : IAuctionInfoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuctionInfoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> CreateAsync(AuctionInfo auction)
        {
            var existingAuction = await _context.AuctionInfos
                .FirstOrDefaultAsync(x => x.CarId == auction.CarId);
            if (existingAuction != null)
            {
                return Result<Unit>.Failure("Main auction already exists");
            }
            _context.AuctionInfos.Add(auction);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to create auction");
            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<Unit>> DeleteAsync(Guid id)
        {
            var auction = await _context.AuctionInfos.FindAsync(id);
            if (auction == null) return Result<Unit>.Failure("Auction not found");
            _context.AuctionInfos.Remove(auction);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to delete auction");
            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<AuctionInfo>> GetAuctionInfoByCarIdAsync(Guid carId)
        {
            var auction = await _context.AuctionInfos.FirstOrDefaultAsync(x => x.CarId == carId);
            if (auction == null) return Result<AuctionInfo>.Failure("Auction not found");
            return Result<AuctionInfo>.Success(auction);
        }
        public async Task<Result<AuctionInfo>> GetAuctionInfoByIdAsync(Guid id)
        {
            var auction = await _context.AuctionInfos.FirstOrDefaultAsync(x => x.Id == id);
            if (auction == null) return Result<AuctionInfo>.Failure("Auction not found");
            return Result<AuctionInfo>.Success(auction);
        }
        public async Task<Result<List<AuctionInfoListItemDto>>> GetAllAuctionsListAsync()
        {
            var auctions = await _context.AuctionInfos.Include(ai => ai.Car).ToListAsync();
            if (auctions == null) return Result<List<AuctionInfoListItemDto>>.Failure("No auctions found");
            return Result<List<AuctionInfoListItemDto>>.Success(_mapper.Map<List<AuctionInfoListItemDto>>(auctions));
        }
    }
}