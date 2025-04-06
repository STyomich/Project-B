using Application.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarService
{
    public class UsersCarListById
    {
        public class Query : IRequest<Result<List<CarListItemDto>>>
        {
            public string? Nickname { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<CarListItemDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<CarListItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = await _context.Users
                    .Where(x => x.UserNickname == request.Nickname)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
                var cars = await _context.Cars
                    .Where(x => x.UserId == userId)
                    .Include(x => x.CarImages)
                    .Include(x => x.CarTopic)
                    .ToListAsync(cancellationToken);
                var carsDto = _mapper.Map<List<CarListItemDto>>(cars);
                return Result<List<CarListItemDto>>.Success(carsDto);
            }
        }
    }
}