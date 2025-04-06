using Core.Domain.Entities;

namespace Core.DTOs.Entities
{
    public class CreateCarWithFormValuesRequest
    {
        public CarTopicDto? CarTopic { get; set; }
        public string RegistrationCountry { get; set; } = string.Empty; // Ukraine
        public string RegistrationText { get; set; } = string.Empty; // AX1234CO
        public string OwnersDescription { get; set; } = string.Empty; // Description of the car from the owner
    }
}