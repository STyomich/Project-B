using Core.Domain.IdentityEntities;

namespace Core.Domain.Entities
{
    public class OrganizationPin
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public bool IsApproved { get; set; } = false;
        public ApplicationUser? User { get; set; }
        public Organization? Organization { get; set; }
    }
}