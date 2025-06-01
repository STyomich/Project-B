using System.Security.Claims;
using Application.Helpers;
using Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserReactionsService
{
    public class CreateUserReaction
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid PostId { get; set; }
            public ClaimsPrincipal? User { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly DataContext _context;
            private readonly UserRepository _userRepository;

            public Handler(DataContext context, UserRepository userRepository)
            {
                _context = context;
                _userRepository = userRepository;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var email = request.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                    return Result<bool>.Failure("User email cannot be null");

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<bool>.Failure("User not found");

                var reaction = new UserReaction
                {
                    PostId = request.PostId,
                    UserId = user.Id,
                };

                // Check if the reaction already exists
                var existingReaction = await _context.UserReactions
                    .FirstOrDefaultAsync(r => r.PostId == request.PostId && r.UserId == user.Id, cancellationToken);
                if (existingReaction != null)
                {
                    _context.UserReactions.Remove(existingReaction);
                    var success = await _context.SaveChangesAsync() > 0;

                    if (!success) return Result<bool>.Failure("Failed to create user reaction");

                    return Result<bool>.Success(false);
                }
                else
                {
                    _context.UserReactions.Add(reaction);
                    var success = await _context.SaveChangesAsync() > 0;

                    if (!success) return Result<bool>.Failure("Failed to create user reaction");

                    return Result<bool>.Success(true);
                }
            }
        }
    }
}