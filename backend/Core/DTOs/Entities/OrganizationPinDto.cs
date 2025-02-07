namespace Core.DTOs.Entities
{
    public class OrganizationPinDto
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}