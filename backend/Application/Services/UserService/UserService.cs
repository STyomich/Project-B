using Application.Helpers;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public UserService(UserRepository userRepository, DataContext dataContext, IMapper mapper)
        {
            _userRepository = userRepository;
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<Result<ApplicationUser>> RegisterUserAsync(RegisterValues userRegister, string role)
        {
            if (string.IsNullOrEmpty(userRegister.Email))
                return Result<ApplicationUser>.Failure("Email cannot be null or empty");

            var existingUser = await _userRepository.GetByEmailAsync(userRegister.Email);

            if (existingUser != null)
                return Result<ApplicationUser>.Failure("Email already exists");

            var user = _mapper.Map<ApplicationUser>(userRegister);
            user.RoleId = _dataContext.Roles.Single(r => r.Name == role).Id;

            if (userRegister.Password == null)
                return Result<ApplicationUser>.Failure("Password cannot be null");

            user.PasswordHash = HashPassword(userRegister.Password);
            await _userRepository.AddUserAsync(user);

            return Result<ApplicationUser>.Success(user);
        }
        public async Task<Result<ApplicationUser>> LoginUserAsync(LoginValues userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Email))
                return Result<ApplicationUser>.Failure("Email cannot be null or empty");

            var user = await _userRepository.GetByEmailAsync(userLogin.Email);

            if (user == null)
                return Result<ApplicationUser>.Failure("User not found");

            if (userLogin.Password == null || user.PasswordHash == null || !VerifyPassword(userLogin.Password, user.PasswordHash))
                return Result<ApplicationUser>.Failure("Invalid password");

            return Result<ApplicationUser>.Success(user);
        }
        public async Task<Result<string>> GetRoleByEmail(string email)
        {
            var user = await _dataContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Role == null)
                return Result<string>.Failure("User or role not found");

            if (string.IsNullOrEmpty(user.Role?.Name))
                return Result<string>.Failure("Role name not found");

            return Result<string>.Success(user.Role.Name);
        }
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}