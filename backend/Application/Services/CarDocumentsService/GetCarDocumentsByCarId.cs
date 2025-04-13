using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.CarDocumentsService
{
    public class GetCarDocumentsByCarId
    {
        public class Query : IRequest<Result<CarDocumentsDto>>
        {
            public Guid CarId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<CarDocumentsDto>>
        {
            private readonly ICarDocumentsService _carDocumentsService;
            private readonly IMapper _mapper;
            public Handler(ICarDocumentsService carDocumentsService, IMapper mapper)
            {
                _mapper = mapper;
                _carDocumentsService = carDocumentsService;
            }
            public async Task<Result<CarDocumentsDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var documents = await _carDocumentsService.GetCarDocumentsByCarIdAsync(request.CarId);
                if (documents.IsSuccess == false)
                {
                    return Result<CarDocumentsDto>.Failure(documents.Error ?? "Car document not found");
                }
                if (documents.Value == null)
                {
                    return Result<CarDocumentsDto>.Failure("Car document value is null");
                }
                return Result<CarDocumentsDto>.Success(_mapper.Map<CarDocumentsDto>(documents.Value));
            }
        }
    }
}