using System.Security.Claims;
using Application.Helpers;
using Application.Interfaces;
using Application.Repositories;
using Core.DTOs.Identity;
using MediatR;

namespace Application.Services.UserService
{
    public class GetCurrentUser
    {
        public class Query : IRequest<Result<UserDto>>
        {
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly IUserService _userService;
            private readonly UserRepository _userRepository;
            private readonly ITokenService _tokenService;
            public Handler(IUserService userService, ITokenService tokenService, UserRepository userRepository)
            {
                _userService = userService;
                _tokenService = tokenService;
                _userRepository = userRepository;
            }

            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.User == null)
                    return Result<UserDto>.Failure("User cannot be null");

                var email = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                    return Result<UserDto>.Failure("User email cannot be null");

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<UserDto>.Failure("User not found");

                var userDto = new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    Role = user.Email != null ? (await _userService.GetRoleByEmail(user.Email)).Value : string.Empty
                };

                return Result<UserDto>.Success(userDto);
            }
        }
    }
}