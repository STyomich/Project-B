using Application.Helpers;
using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;
using Core.Enums;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Identity
{
    public class RegisterViaIdentity
    {
        public class Command : IRequest<Result<UserDto>>
        {
            public RegisterViaIdentityValues? Values { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<UserDto>>
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
            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Values == null || string.IsNullOrEmpty(request.Values.Password))
                {
                    return Result<UserDto>.Failure("Invalid registration values");
                }

                var user = _mapper.Map<ApplicationUser>(request.Values);
                var result = await _userManager.CreateAsync(user, request.Values.Password);
                await _userManager.AddToRoleAsync(user, RolesEnum.User.ToString()); // Permanent add all users to User group.

                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    userDto.Token = _tokenService.CreateToken(user);
                    return Result<UserDto>.Success(userDto);
                }

                return Result<UserDto>.Failure("Failed to register user");
            }
        }
    }
}