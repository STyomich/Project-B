using Application.Helpers;
using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Identity
{
    public class LoginViaIdentity
    {
        public class Query : IRequest<Result<UserDto>>
        {
            public LoginViaIdentityValues? Values { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;
            private readonly TokenService _tokenService;
            public Handler(UserManager<ApplicationUser> userManager, IMapper mapper, TokenService tokenService)
            {
                _userManager = userManager;
                _mapper = mapper;
                _tokenService = tokenService;
            }
            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                if (request.Values == null || string.IsNullOrEmpty(request.Values.Email))
                {
                    return Result<UserDto>.Failure("Invalid email or password");
                }

                var user = await _userManager.FindByEmailAsync(request.Values.Email);
                if (user == null)
                {
                    return Result<UserDto>.Failure("Invalid email or password");
                }

                if (string.IsNullOrEmpty(request.Values.Password))
                {
                    return Result<UserDto>.Failure("Invalid email or password");
                }

                var result = await _userManager.CheckPasswordAsync(user, request.Values.Password);
                if (!result)
                {
                    return Result<UserDto>.Failure("Invalid email or password");
                }

                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(user);

                return Result<UserDto>.Success(userDto);
            }
        }
    }
}