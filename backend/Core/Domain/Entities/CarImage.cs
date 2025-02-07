namespace Core.Domain.Entities
{
    public class CarImage
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string? ImageUrl { get; set; }
        public bool isMain { get; set; } = false;
        public Car? Car { get; set; }
    }
}