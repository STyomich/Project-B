using Application.Helpers;
using AutoMapper;
using Core.DTOs.Identity;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.OrganizationService
{
    public class OrganizationMembersList
    {
        public class Query : IRequest<Result<List<UserListItemDto>>>
        {
            public Guid OrganizationId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<UserListItemDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<UserListItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _context.Users
                    .Where(u => _context.OrganizationPins
                    .Any(op => op.UserId == u.Id && op.OrganizationId == request.OrganizationId))
                    .ToListAsync();
                var userListItemDto = _mapper.Map<List<UserListItemDto>>(users);
                return Result<List<UserListItemDto>>.Success(userListItemDto);
            }
        }
    }
}