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
            public Guid UserId { get; set; }
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
                var cars = await _context.Cars
                    .Where(x => x.UserId == request.UserId)
                    .ProjectTo<CarListItemDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                var carsDto = _mapper.Map<List<CarListItemDto>>(cars);
                return Result<List<CarListItemDto>>.Success(carsDto);
            }
        }
    }
}