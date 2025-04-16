namespace Core.DTOs.Entities
{
    public class AuctionBidDto
    {
        public Guid AuctionInfoId { get; set; }
        public Guid UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
    }
}