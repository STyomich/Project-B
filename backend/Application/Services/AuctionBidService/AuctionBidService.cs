using Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuctionBidService
{
    public class AuctionBidService : IAuctionBidService
    {
        private readonly DataContext _context;
        public AuctionBidService(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> PlaceBid(AuctionBid auctionBid)
        {
            var auction = await _context.AuctionInfos.FindAsync(auctionBid.AuctionInfoId);
            if (auction == null) return false;

            if (auctionBid.BidAmount <= auction.StartPrice || auctionBid.BidAmount >= auction.BuyoutPrice) return false;

            _context.AuctionBids.Add(auctionBid);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<AuctionBid>> GetBidsByAuctionId(Guid auctionId)
        {
            var bids = await _context.AuctionBids
                .Where(b => b.AuctionInfoId == auctionId)
                .ToListAsync();
            return bids;
        }
        public async Task<bool> DeleteBid(Guid bidId)
        {
            var bid = await _context.AuctionBids.FindAsync(bidId);
            if (bid == null) return false;

            _context.AuctionBids.Remove(bid);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<AuctionBid?> GetMaxAuctionBidByAuctionId(Guid auctionId)
        {
            var maxBid = await _context.AuctionBids
                .Where(b => b.AuctionInfoId == auctionId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();
            return maxBid;
        }
    }
}