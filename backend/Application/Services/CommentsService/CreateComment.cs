using System.Security.Claims;
using Application.Helpers;
using Application.Repositories;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CommentsService
{
    public class CreateComment
    {
        public class Command : IRequest<Result<CommentDto>>
        {
            public CommentDto? CommentDto;
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CommentDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserRepository _userRepository;

            public Handler(DataContext context, IMapper mapper, UserRepository userRepository)
            {
                _context = context;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<Result<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var email = request.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                    return Result<CommentDto>.Failure("User email cannot be null");

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<CommentDto>.Failure("User not found");
                if (request.CommentDto == null)
                {
                    return Result<CommentDto>.Failure("Comment data is missing");
                }
                request.CommentDto.UserId = user.Id;
                request.CommentDto.CreatedAt = DateTime.UtcNow;
                var comment = _mapper.Map<Comment>(request.CommentDto);
                _context.Comments.Add(comment);
                
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success)
                {
                    return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
                }
                
                return Result<CommentDto>.Failure("Failed to create comment");
            }
        }
    }
}