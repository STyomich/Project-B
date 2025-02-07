using FluentValidation;

namespace Core.DTOs.Entities
{
    public class CarDocumentsDto
    {
        public Guid Id { get; set; }
        public string? Url {get; set;}
        public bool IsApproved { get; set; } = false;
    }
    public class CarDocumentsValidator : AbstractValidator<CarDocumentsDto>
    {
        public CarDocumentsValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url is required");
        }
    }
}