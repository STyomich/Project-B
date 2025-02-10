using Application.Helpers;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CarService
{
    public class CreateNewCar
    {
        public class Command : IRequest<Result<CarDto>>
        {
            public CarDto? Car { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CarDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            } 
            public async Task<Result<CarDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Car == null)
                {
                    return Result<CarDto>.Failure("Car is required");
                }
                var car = _mapper.Map<Car>(request.Car);
                await _context.Cars.AddAsync(car);
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    return Result<CarDto>.Failure("Failed to create car");
                }
                return Result<CarDto>.Success(_mapper.Map<CarDto>(car));
            }
        }
    }
}