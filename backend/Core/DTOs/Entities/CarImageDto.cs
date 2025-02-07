using FluentValidation;

namespace Core.DTOs.Entities
{
    public class CarImageDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string? ImageUrl { get; set; }
        public bool isMain { get; set; } = false;
    }
    public class CarImageValidator : AbstractValidator<CarImageDto>
    {
        public CarImageValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
        }
    }
}