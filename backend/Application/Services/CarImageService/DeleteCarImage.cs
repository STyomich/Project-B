using Application.Helpers;
using Application.Interfaces;
using MediatR;

namespace Application.Services.CarImageService
{
    public class DeleteCarImage
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var result = await _carImageService.DeleteAsync(request.Id);
                if (result.IsSuccess)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                return Result<Unit>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}