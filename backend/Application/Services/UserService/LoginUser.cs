using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.DTOs.Identity;
using MediatR;

namespace Application.Services.UserService
{
    public class LoginUser
    {
        public class Query : IRequest<Result<UserDto>>
        {
            public LoginValues? Values { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly IUserService _userService;
            private readonly ITokenService _tokenService;
            private readonly IMapper _mapper;
            public Handler(IUserService userService, ITokenService tokenService, IMapper mapper)
            {
                _userService = userService;
                _tokenService = tokenService;
                _mapper = mapper;
            }

            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.Values == null)
                    return Result<UserDto>.Failure("Login values cannot be null");

                var result = await _userService.LoginUserAsync(request.Values);
                if (!result.IsSuccess)
                    return Result<UserDto>.Failure(result.Error ?? "Unknown error");
                var user = _mapper.Map<UserDto>(result.Value);

                if (result.Value == null)
                    return Result<UserDto>.Failure("User not found");

                user.Token = _tokenService.CreateToken(result.Value);

                if (string.IsNullOrEmpty(user.Email))
                    return Result<UserDto>.Failure("User email cannot be null or empty");

                user.Role = (await _userService.GetRoleByEmail(user.Email)).Value;

                return Result<UserDto>.Success(user);
            }
        }
    }
}