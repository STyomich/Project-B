namespace Core.Domain.Entities
{
    public class RegistrationPlate
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string? Country { get; set; } // Ukraine
        public string? Text { get; set; } // AX1234CO
        public Car? Car { get; set; }
    }
}