using Application.Helpers;
using Application.Interfaces;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.CarImageService
{
    public class GetMainCarImageByCarId
    {
        public class Query : IRequest<Result<CarImageDto>>
        {
            public Guid CarId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<CarImageDto>>
        {
            private readonly ICarImageService _carImageService;
            public Handler(ICarImageService carImageService)
            {
                _carImageService = carImageService;
            }

            public async Task<Result<CarImageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _carImageService.GetMainCarImageByCarIdAsync(request.CarId);
                if (result.IsSuccess)
                {
                    if (result.Value != null)
                    {
                        return Result<CarImageDto>.Success(await _carImageService.ToDto(result.Value));
                    }
                    return Result<CarImageDto>.Failure("Car image not found");
                }
                return Result<CarImageDto>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}