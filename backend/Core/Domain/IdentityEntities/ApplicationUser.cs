namespace Core.Domain.IdentityEntities
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? UserNickname { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? AvatarUrl { get; set; }
        public Role? Role { get; set; }
    }
}