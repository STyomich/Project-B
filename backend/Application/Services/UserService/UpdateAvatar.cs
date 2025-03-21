using System.Security.Claims;
using Application.Helpers;
using Application.Interfaces;
using Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.UserService
{
    public class UpdateAvatar
    {
        public class Command : IRequest<Result<string>>
        {
            public IFormFile? File { get; set; }
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IUserService _userService;
            private readonly UserRepository _userRepository;
            public Handler(IUserService userService, UserRepository userRepository)
            {
                _userService = userService;
                _userRepository = userRepository;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.File == null)
                    return Result<string>.Failure("File cannot be null");

                if (request.User == null)
                    return Result<string>.Failure("User cannot be null");

                var email = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                if (email == null)
                    return Result<string>.Failure("User email cannot be null");

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<string>.Failure("User not found");

                var result = await _userService.UpdateAvatarAsync(request.File, email);
                if (result.IsSuccess)
                {
                    if (result.Value == null)
                        return Result<string>.Failure("Error updating avatar");
                    return Result<string>.Success(result.Value);
                }
                else
                {
                    return Result<string>.Failure("Error updating avatar");
                }
            }
        }
    }
}