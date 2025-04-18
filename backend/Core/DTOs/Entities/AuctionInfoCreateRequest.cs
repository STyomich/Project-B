namespace Core.DTOs.Entities
{
    public class AuctionInfoCreateRequest
    {
        public Guid CarId { get; set; }
        public decimal StartPrice { get; set; }
        public decimal BuyoutPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}