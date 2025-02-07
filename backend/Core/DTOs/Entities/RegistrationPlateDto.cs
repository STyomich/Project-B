using FluentValidation;

namespace Core.DTOs.Entities
{
    public class RegistrationPlateDto
    {
        public Guid Id { get; set; }
        public string? Country { get; set; } // Ukraine
        public string? Text { get; set; } // AX1234C
    }
    public class RegistrationPlateValidator : AbstractValidator<RegistrationPlateDto>
    {
        public RegistrationPlateValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required");
        }
    }
}