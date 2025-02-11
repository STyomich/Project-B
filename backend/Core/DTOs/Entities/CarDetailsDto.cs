using Core.DTOs.Identity;

namespace Core.DTOs.Entities
{
    public class CarDetailsDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarTopicId { get; set; }
        public string? OwnersDescription { get; set; }
        public UserDto? User { get; set; }
        public CarTopicDto? CarTopic { get; set; }
        public ICollection<CarImageDto>? CarImages { get; set; }
        public RegistrationPlateDto? RegistrationPlate { get; set; }
        public CarDocumentsDto? CarDocuments { get; set; }
    }
}