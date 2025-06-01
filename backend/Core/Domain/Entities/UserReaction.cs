using Core.Domain.IdentityEntities;

namespace Core.Domain.Entities
{
    public class UserReaction
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public ApplicationUser? User { get; set; }
        public Post? Post { get; set; }
    }
}