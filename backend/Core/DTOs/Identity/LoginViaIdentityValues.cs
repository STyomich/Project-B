using FluentValidation;

namespace Core.DTOs.Identity
{
    public class LoginViaIdentityValues
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginViaIdentityValuesValidator : AbstractValidator<LoginViaIdentityValues>
    {
        public LoginViaIdentityValuesValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}