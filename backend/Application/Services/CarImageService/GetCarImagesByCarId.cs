using Application.Helpers;
using Application.Interfaces;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.CarImageService
{
    public class GetCarImagesByCarId
    {
        public class Query : IRequest<Result<List<CarImageDto>>>
        {
            public Guid CarId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<CarImageDto>>>
        {
            private readonly ICarImageService _carImageService;
            public Handler(ICarImageService carImageService)
            {
                _carImageService = carImageService;
            }

            public async Task<Result<List<CarImageDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _carImageService.GetCarImagesByCarIdAsync(request.CarId);
                if (result.IsSuccess)
                {
                    if (result.Value != null)
                    {
                        return Result<List<CarImageDto>>.Success(await _carImageService.ToDto(result.Value));
                    }
                    return Result<List<CarImageDto>>.Failure("Car image not found");
                }
                return Result<List<CarImageDto>>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}