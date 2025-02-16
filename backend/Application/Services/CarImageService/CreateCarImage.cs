using Application.Helpers;
using Application.Interfaces;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.CarImageService
{
    public class CreateCarImage
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CarImageDto? carImageDto { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ICarImageService _carImageService;

            public Handler(ICarImageService carImageService)
            {
                _carImageService = carImageService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.carImageDto == null)
                {
                    return Result<Unit>.Failure("Car image not found");
                }
                var result = await _carImageService.CreateAsync(request.carImageDto);
                if (result.IsSuccess)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                return Result<Unit>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}