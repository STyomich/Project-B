using FluentValidation;

namespace Core.DTOs.Entities
{
    public class CarTopicDto
    {
        public Guid Id { get; set; }
        public string? CarName { get; set; } // Toyota, Ferrari, Lambo...
        public string? CarModel { get; set; } // Corolla, 458 GTB, Countach...
        public string? CarYear { get; set; } // 2021, 2020, 2019...
        public string? Description { get; set; } // Description of the car
        public string? ImageLogoUrl { get; set; } // Image logo URL of the CarName (Toyota, Ferrari...)
    }
    public class CarTopicValidator : AbstractValidator<CarTopicDto>
    {
        public CarTopicValidator()
        {
            RuleFor(x => x.CarName).NotEmpty().WithMessage("CarName is required");
            RuleFor(x => x.CarModel).NotEmpty().WithMessage("CarModel is required");
            RuleFor(x => x.CarYear).NotEmpty().WithMessage("CarYear is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImageLogoUrl).NotEmpty().WithMessage("ImageLogoUrl is required");
        }
    }
}