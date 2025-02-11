using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarService
{
    public class GetCarDetailsById
    {
        public class Query : IRequest<Result<CarDetailsDto>>
        {
            public Guid CarId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<CarDetailsDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<CarDetailsDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var car = await _context.Cars
                    .Include(c => c.User)
                    .Include(c => c.CarTopic)
                    .Include(c => c.CarImages)
                    .Include(c => c.RegistrationPlate)
                    .Include(c => c.CarDocuments)
                    .FirstOrDefaultAsync(c => c.Id == request.CarId);
                if (car == null) return Result<CarDetailsDto>.Failure("Car not found");
                var carDetailsDto = _mapper.Map<CarDetailsDto>(car);
                return Result<CarDetailsDto>.Success(carDetailsDto);
            }
        }
    }
}