using Core.Domain.IdentityEntities;

namespace Core.Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarTopicId { get; set; }
        public string? OwnersDescription { get; set; }
        public ApplicationUser? User { get; set; }
        public CarTopic? CarTopic { get; set; }
        public ICollection<CarImage>? CarImages { get; set; }
        public RegistrationPlate? RegistrationPlate { get; set; }
        public CarDocuments? CarDocuments { get; set; }
    }
}