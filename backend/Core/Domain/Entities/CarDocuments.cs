namespace Core.Domain.Entities
{
    public class CarDocuments
    {
        public Guid Id { get; set; }
        public string? Url {get; set;}
        public bool IsApproved { get; set; } = false;
        public Car? Car { get; set; }
    }
}