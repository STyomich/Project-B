using Core.Domain.Entities;

namespace Core.DTOs.Entities
{
    public class CarListItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarTopicId { get; set; }
        public string? OwnersDescription { get; set; }
        public CarTopicDto? CarTopic { get; set; }
        public CarImageDto? CarMainImage { get; set; }
        public RegistrationPlateDto? RegistrationPlate { get; set; }
    }
}