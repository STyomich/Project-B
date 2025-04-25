namespace Core.Domain.Entities
{
    public class AuctionInfo
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public decimal StartPrice { get; set; }
        public decimal BuyoutPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public Car? Car { get; set; }
        public ICollection<AuctionBid>? AuctionBids { get; set; }
    }
}