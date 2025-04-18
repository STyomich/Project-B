using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuctionBidService
    {
        Task<bool> PlaceBid(AuctionBid auctionBid);
        Task<List<AuctionBid>> GetBidsByAuctionId(Guid auctionId);
        Task<bool> DeleteBid(Guid bidId);
        Task<AuctionBid?> GetMaxAuctionBidByAuctionId(Guid auctionId);
    }
}