using Core.Domain.IdentityEntities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<ApplicationUser?> GetByUserNicknameAsync(string userNickname)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserNickname == userNickname);
        }
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}