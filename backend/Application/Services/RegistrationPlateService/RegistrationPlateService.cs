using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.RegistrationPlateService
{
    public class RegistrationPlateService : IRegistrationPlateService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RegistrationPlateService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> CreateAsync(RegistrationPlateDto dto)
        {
            var plate = _mapper.Map<RegistrationPlate>(dto);
            if (_context.RegistrationPlates.Any(x => x.CarId == plate.CarId)) return Result<Unit>.Failure("Car already has a registration plate");
            _context.RegistrationPlates.Add(plate);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to create registration plate");
            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<Unit>> UpdateAsync(Guid id, RegistrationPlateDto dto)
        {
            var plate = await _context.RegistrationPlates.FindAsync(id);
            if (plate == null) return Result<Unit>.Failure("Registration plate not found");

            _mapper.Map(dto, plate);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to create registration plate");
            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<RegistrationPlate>> GetByCarIdAsync(Guid carId)
        {
            var plate = await _context.RegistrationPlates.FirstOrDefaultAsync(x => x.CarId == carId);
            if (plate == null) return Result<RegistrationPlate>.Failure("Registration plate not found");
            return Result<RegistrationPlate>.Success(plate);
        }
        public Task<RegistrationPlateDto> ToDto(RegistrationPlate plate)
        {
            return Task.FromResult(_mapper.Map<RegistrationPlateDto>(plate));
        }
    }
}