using Application.Helpers;
using Application.Interfaces;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.CarDocumentsService
{
    public class CreateCarDocuments
    {
        public class Command : IRequest<Result<Unit>>
        {
            public IFormFile? Document { get; set; }
            public Guid CarId { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ICarDocumentsService _carDocumentsService;
            private readonly IPdfService _pdfService;
            public Handler(ICarDocumentsService carDocumentsService, IPdfService pdfService)
            {
                _carDocumentsService = carDocumentsService;
                _pdfService = pdfService;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Document == null)
                {
                    return Result<Unit>.Failure("Car document not found");
                }
                var uploadResult = await _pdfService.AddPdfAsync(request.Document);
                if (uploadResult.Error != null)
                {
                    return Result<Unit>.Failure(uploadResult.Error.Message);
                }
                var document = new CarDocuments
                {
                    CarId = request.CarId,
                    Url = uploadResult.Url.ToString(),
                    IsApproved = false,
                };
                var result = await _carDocumentsService.CreateAsync(document);
                if (result.IsSuccess)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                return Result<Unit>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}