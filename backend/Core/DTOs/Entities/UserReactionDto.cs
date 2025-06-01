using Core.DTOs.Identity;

namespace Core.DTOs.Entities
{
    public class UserReactionDto
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public UserShortInfo? User { get; set; }
        public PostDto? Post { get; set; }
    }
}