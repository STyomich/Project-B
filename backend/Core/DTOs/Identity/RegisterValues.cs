using FluentValidation;

namespace Core.DTOs.Identity
{
    public class RegisterValues
    {
        public string? Email { get; set; }
        public string? UserNickname { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? Password { get; set; }
    }
    public class RegisterViaIdentityValuesValidator : AbstractValidator<RegisterValues>
    {
        public RegisterViaIdentityValuesValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.UserNickname).NotEmpty().WithMessage("UserNickname is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.UserSurname).NotEmpty().WithMessage("UserSurname is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}