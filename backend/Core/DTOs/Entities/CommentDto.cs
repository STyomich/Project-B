using Core.DTOs.Identity;

namespace Core.DTOs.Entities
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserShortInfo? User { get; set; }
    }
}