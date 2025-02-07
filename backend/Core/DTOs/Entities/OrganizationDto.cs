using FluentValidation;

namespace Core.DTOs.Entities
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public Guid AdministratorId { get; set; } // Id of user can manage this organization.
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageLogoUrl { get; set; }
    }
    public class OrganizationValidator : AbstractValidator<OrganizationDto>
    {
        public OrganizationValidator()
        {
            RuleFor(x => x.AdministratorId).NotEmpty().WithMessage("AdministratorId is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImageLogoUrl).NotEmpty().WithMessage("ImageLogoUrl is required");
        }
    }
}