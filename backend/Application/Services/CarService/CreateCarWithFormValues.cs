using System.Security.Claims;
using Application.Helpers;
using Application.Repositories;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CarService
{
    public class CreateCarWithFormValues
    {
        public class Command : IRequest<Result<CarDto>>
        {
            public CreateCarWithFormValuesRequest? Request { get; set; }
            public ClaimsPrincipal? User { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CarDto>>
        {
            private readonly DataContext _context;
            private readonly UserRepository _userRepository;
            public Handler(DataContext context, UserRepository userRepository)
            {
                _userRepository = userRepository;
                _context = context;
            }
            public async Task<Result<CarDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Request == null)
                    return Result<CarDto>.Failure("Request cannot be null");

                var email = request.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                    return Result<CarDto>.Failure("User email cannot be null");

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    return Result<CarDto>.Failure("User not found");
                

                var car = new Car
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    CarTopicId = request.Request.CarTopic?.Id ?? Guid.Empty,
                    OwnersDescription = request.Request.OwnersDescription,
                };

                _context.Cars.Add(car);
                var registrationPlate = new RegistrationPlate
                {
                    Id = Guid.NewGuid(),
                    CarId = car.Id,
                    Country = request.Request.RegistrationCountry,
                    Text = request.Request.RegistrationText,
                };
                _context.RegistrationPlates.Add(registrationPlate);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Result<CarDto>.Success(new CarDto
                    {
                        Id = car.Id,
                        UserId = user.Id,
                        CarTopicId = car.CarTopicId,
                        OwnersDescription = car.OwnersDescription,
                    });
                }
                else
                {
                    return Result<CarDto>.Failure("Failed to create car");
                }
            }
        }
    }
}