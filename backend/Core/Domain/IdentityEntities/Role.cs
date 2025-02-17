namespace Core.Domain.IdentityEntities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}