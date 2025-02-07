namespace Core.Domain.Entities
{
    public class Organization
    {
        public Guid Id { get; set; }
        public Guid AdministratorId { get; set; } // Id of user can manage this organization.
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageLogoUrl { get; set; }
        public ICollection<OrganizationPin>? OrganizationPins { get; set; }
    }
}