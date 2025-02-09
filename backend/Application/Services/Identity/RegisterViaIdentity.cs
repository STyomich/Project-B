using Application.Helpers;
using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;
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
            private readonly DataContext _dataContext;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;
            private readonly TokenService _tokenService;
            public Handler(DataContext dataContext, UserManager<ApplicationUser> userManager, IMapper mapper, TokenService tokenService)
            {
                _dataContext = dataContext;
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
                // TODO: Implement appending user to role.

                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return Result<UserDto>.Success(userDto);
                }

                return Result<UserDto>.Failure("Failed to register user");
            }
        }
    }
}