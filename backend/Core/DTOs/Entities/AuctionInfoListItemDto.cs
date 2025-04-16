namespace Core.DTOs.Entities
{
    public class AuctionInfoListItemDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public decimal StartPrice { get; set; }
        public decimal BuyoutPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarListItemDto? Car { get; set; }
    }
}