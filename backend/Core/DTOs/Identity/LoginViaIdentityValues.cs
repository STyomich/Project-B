using FluentValidation;

namespace Core.DTOs.Identity
{
    public class LoginValues
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginViaIdentityValuesValidator : AbstractValidator<LoginValues>
    {
        public LoginViaIdentityValuesValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}