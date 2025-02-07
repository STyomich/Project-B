using FluentValidation;

namespace Core.DTOs.Entities
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarTopicId { get; set; }
        public Guid RegistrationPlateId { get; set; }
        public Guid CarDocumentsId { get; set; }
        public string? OwnersDescription { get; set; }

    }
    public class CarValidator : AbstractValidator<CarDto>
    {
        public CarValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.CarTopicId).NotEmpty().WithMessage("CarTopicId is required");
            RuleFor(x => x.RegistrationPlateId).NotEmpty().WithMessage("RegistrationPlateId is required");
            RuleFor(x => x.CarDocumentsId).NotEmpty().WithMessage("CarDocumentsId is required");
            RuleFor(x => x.OwnersDescription).NotEmpty().WithMessage("OwnersDescription is required");
        }
    }
}