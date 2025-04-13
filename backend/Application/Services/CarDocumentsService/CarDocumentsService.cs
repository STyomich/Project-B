using Application.Helpers;
using Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarDocumentsService
{
    public class CarDocumentsService : ICarDocumentsService
    {
        private readonly DataContext _context;
        public CarDocumentsService(DataContext context)
        {
            _context = context;
        }
        public async Task<Result<Unit>> CreateAsync(CarDocuments carDocuments)
        {
            var existingMainDocument = _context.CarDocuments
                .FirstOrDefault(x => x.CarId == carDocuments.CarId);
            if (existingMainDocument != null)
            {
                return Result<Unit>.Failure("Main car document already exists");
            }
            _context.CarDocuments.Add(carDocuments);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to create car document");
            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<Unit>> DeleteAsync(Guid id)
        {
            var document = await _context.CarDocuments.FindAsync(id);
            if (document == null) return Result<Unit>.Failure("Car document not found");
            _context.CarDocuments.Remove(document);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to delete car document");
            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<CarDocuments>> GetCarDocumentsByCarIdAsync(Guid carId)
        {
            var documents = await _context.CarDocuments.FirstOrDefaultAsync(x => x.CarId == carId);
            if (documents == null) return Result<CarDocuments>.Failure("Car document not found");
            return Result<CarDocuments>.Success(documents);
        }
    }
}