using Application.Helpers;
using Application.Interfaces;
using MediatR;

namespace Application.Services.CarDocumentsService
{
    public class DeleteCarDocuments
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var document = await _carDocumentsService.GetCarDocumentsByCarIdAsync(request.Id);
                if (document.IsSuccess == false)
                {
                    return Result<Unit>.Failure(document.Error ?? "Car document not found");
                }
                if (document.Value == null || string.IsNullOrEmpty(document.Value.Url))
                {
                    return Result<Unit>.Failure("Document URL is null or empty");
                }
                var cloudinaryResult = await _pdfService.DeletePdfAsync(document.Value.Url);
                if (cloudinaryResult == "ok")
                {
                    var result = await _carDocumentsService.DeleteAsync(request.Id);
                    if (result.IsSuccess)
                    {
                        return Result<Unit>.Success(Unit.Value);
                    }
                }
                return Result<Unit>.Failure("Unknown error");
            }
        }
    }
}