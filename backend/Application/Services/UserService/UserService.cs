using Application.Helpers;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IImageService _imageService;
        public UserService(UserRepository userRepository, DataContext dataContext, IMapper mapper, IImageService imageService)
        {
            _userRepository = userRepository;
            _dataContext = dataContext;
            _mapper = mapper;
            _imageService = imageService;
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
        public async Task<Result<string>> UpdateAvatarAsync(IFormFile file, string email)
        {
            try
            {
                var uploadResult = await _imageService.AddImageAsync(file);

                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != default)
                {
                    user.AvatarUrl = uploadResult.Url.ToString();
                    await _dataContext.SaveChangesAsync();
                }
                if (user != null && user.AvatarUrl != null)
                    return Result<string>.Success(user.AvatarUrl);
                else
                    return Result<string>.Failure("Error updating avatar");
            }
            catch (Exception e)
            {
                return Result<string>.Failure(e.Message);
            }
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
        public async Task<Result<Guid>> GetUserIdByNickname(string nickname)
        {
            var user = await _dataContext.Users
                .FirstOrDefaultAsync(u => u.UserNickname == nickname);

            if (user == null)
                return Result<Guid>.Failure("User not found");

            return Result<Guid>.Success(user.Id);
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