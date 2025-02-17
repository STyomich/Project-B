using Core.Domain.IdentityEntities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}