using Application.Helpers;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<ApplicationUser>> RegisterUserAsync(RegisterValues userRegister, string role);
        Task<Result<ApplicationUser>> LoginUserAsync(LoginValues userLogin);
        Task<Result<string>> GetRoleByEmail(string email);
    }
}