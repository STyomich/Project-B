namespace Core.DTOs.Entities
{
    public class AuctionInfoDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public decimal StartPrice { get; set; }
        public decimal BuyoutPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarDetailsDto? Car { get; set; }
        public AuctionBidDto? MaxBid { get; set; }
    }
}