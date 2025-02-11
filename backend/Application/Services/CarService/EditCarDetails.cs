using Application.Helpers;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarService
{
    public class EditCarDetails
    {
        public class Command : IRequest<Result<CarDetailsDto>>
        {
            public Guid CarId { get; set; }
            public CarDto? CarDto { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CarDetailsDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<CarDetailsDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var car = await _context.Cars
                    .FirstOrDefaultAsync(c => c.Id == request.CarId);
                if (car == null) return Result<CarDetailsDto>.Failure("Car not found");
                if (request.CarDto != null)
                {
                    car = _mapper.Map<Car>(request.CarDto);
                    await _context.SaveChangesAsync();
                }
                var carDetailsDto = _mapper.Map<CarDetailsDto>(car);
                return Result<CarDetailsDto>.Success(carDetailsDto);
            }
        }
    }
}