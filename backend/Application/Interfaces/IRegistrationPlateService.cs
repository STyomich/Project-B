using Application.Helpers;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Interfaces
{
    public interface IRegistrationPlateService
    {
        Task<Result<Unit>> CreateAsync(RegistrationPlateDto dto);
        Task<Result<Unit>> UpdateAsync(Guid id, RegistrationPlateDto dto);
        Task<Result<RegistrationPlate>> GetByCarIdAsync(Guid carId);
        Task<RegistrationPlateDto> ToDto(RegistrationPlate plate);
    }
}