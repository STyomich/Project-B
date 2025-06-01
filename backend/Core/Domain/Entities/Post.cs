using Core.Domain.IdentityEntities;

namespace Core.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<UserReaction>? UserReactions { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}