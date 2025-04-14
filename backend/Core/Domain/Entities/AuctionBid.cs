using Core.Domain.IdentityEntities;

namespace Core.Domain.Entities
{
    public class AuctionBid
    {
        public Guid AuctionInfoId { get; set; }
        public Guid UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public AuctionInfo? AuctionInfo { get; set; }
        public ApplicationUser? User { get; set; }
    }
}