namespace Core.Domain.Entities
{
    public class Documents
    {
        public Guid Id { get; set; }
        public string? Url {get; set;}
        public bool IsApproved { get; set; } = false;
    }
}