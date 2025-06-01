using System.Security.Claims;
using Application.Helpers;
using Application.Repositories;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.PostsService
{
    public class CreatePost
    {
        public class Command : IRequest<Result<PostDto>>
        {
            public PostDto? PostDto { get; set; }
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<PostDto>>
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

            public async Task<Result<PostDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.PostDto == null)
                {
                    return Result<PostDto>.Failure("Post not found");
                }
                var email = request.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                    return Result<PostDto>.Failure("User email cannot be null");

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<PostDto>.Failure("User not found");
                request.PostDto.UserId = user.Id;
                var post = _mapper.Map<Post>(request.PostDto);
                post.CreatedAt = DateTime.UtcNow;
                _context.Posts.Add(post);
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    return Result<PostDto>.Success(_mapper.Map<PostDto>(post));
                }
                return Result<PostDto>.Failure("Failed to create post");
            }
        }
    }
}