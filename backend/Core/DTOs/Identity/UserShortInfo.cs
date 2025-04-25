using Core.DTOs.Entities;

namespace Core.DTOs.Identity
{
    public class UserShortInfo
    {
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? UserNickname { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public OrganizationDto? Organization { get; set; }
    }
}