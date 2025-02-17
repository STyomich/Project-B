using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.DTOs.Identity;
using Core.Enums;
using MediatR;

namespace Application.Services.UserService
{
    public class RegisterUser
    {
        public class Command : IRequest<Result<UserDto>>
        {
            public RegisterValues? UserRegister { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<UserDto>>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            private readonly ITokenService _tokenService;
            public Handler(IUserService userService, IMapper mapper, ITokenService tokenService)
            {
                _userService = userService;
                _mapper = mapper;
                _tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.UserRegister == null)
                    return Result<UserDto>.Failure("User registration values cannot be null");

                var result = await _userService.RegisterUserAsync(request.UserRegister, RolesEnum.User.ToString());
                if (!result.IsSuccess)
                    return Result<UserDto>.Failure(result.Error ?? "Unknown error");
                var user = _mapper.Map<UserDto>(result.Value);
                if (result.Value != null)
                    user.Token = _tokenService.CreateToken(result.Value);

                if (user.Email != null)
                    user.Role = _userService.GetRoleByEmail(user.Email).Result.Value;
                else
                    return Result<UserDto>.Failure("User email cannot be null");

                return Result<UserDto>.Success(user);
            }
        }
    }
}