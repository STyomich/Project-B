using System.Security.Claims;
using Application.Helpers;
using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;
using Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Identity
{
    public class GetCurrentUser
    {
        public class Query : IRequest<Result<UserDto>>
        {
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly TokenService _tokenService;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(TokenService tokenService, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _tokenService = tokenService;
                _mapper = mapper;
                _userManager = userManager;
            }
            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.User == null)
                {
                    return Result<UserDto>.Failure("User not found");
                }
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == request.User.FindFirstValue(ClaimTypes.Email));
                if (user != null)
                {
                    string role = "User";
                    if (await _userManager.IsInRoleAsync(user, RolesEnum.Administrator.ToString()))
                        role = "Administrator";
                    var userDto = _mapper.Map<UserDto>(user);
                    userDto.Token = _tokenService.CreateToken(user);
                    userDto.Role = role;
                    return Result<UserDto>.Success(userDto);
                }
                else
                    return Result<UserDto>.Failure("User not found");
            }
        }
    }
}