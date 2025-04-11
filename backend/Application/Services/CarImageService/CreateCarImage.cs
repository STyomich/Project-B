using Application.Helpers;
using Application.Interfaces;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.CarImageService
{
    public class CreateCarImage
    {
        public class Command : IRequest<Result<Unit>>
        {
            public IFormFile? Image { get; set; }
            public Guid CarId { get; set; }
            public bool IsMain { get; set; } = false;
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ICarImageService _carImageService;
            private readonly IImageService _imageService;
            public Handler(ICarImageService carImageService, IImageService imageService)
            {
                _carImageService = carImageService;
                _imageService = imageService;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Image == null)
                {
                    return Result<Unit>.Failure("Car image not found");
                }

                var uploadResult = await _imageService.AddImageAsync(request.Image);
                if (uploadResult.Error != null)
                {
                    return Result<Unit>.Failure(uploadResult.Error.Message);
                }
                var image = new CarImage
                {
                    CarId = request.CarId,
                    ImageUrl = uploadResult.Url.ToString(),
                    isMain = request.IsMain
                };
                var result = await _carImageService.CreateAsync(image);
                if (result.IsSuccess)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                return Result<Unit>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}