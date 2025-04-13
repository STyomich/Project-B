using Application.Helpers;
using Core.Domain.Entities;
using MediatR;

namespace Application.Interfaces
{
    public interface ICarDocumentsService
    {
        Task<Result<Unit>> CreateAsync(CarDocuments carDocuments);
        Task<Result<Unit>> DeleteAsync(Guid id);
        Task<Result<CarDocuments>> GetCarDocumentsByCarIdAsync(Guid carId);
    }
}