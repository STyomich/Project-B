using Core.DTOs.Identity;

namespace Core.DTOs.Entities
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ReactionsCount { get; set; }
        public UserShortInfo? User { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
    }
}