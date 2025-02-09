using Microsoft.AspNetCore.Identity;

namespace Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? UserSurname { get; set; }
        public string? UserNickname { get; set; }
        public string? AvatarUrl { get; set; }
    }
}