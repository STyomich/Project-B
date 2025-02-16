using Application.Helpers;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Interfaces
{
    public interface ICarImageService
    {
        Task<Result<Unit>> CreateAsync(CarImageDto dto);
        Task<Result<Unit>> DeleteAsync(Guid id);
        Task<Result<CarImage>> GetMainCarImageByCarIdAsync(Guid carId);
        Task<Result<List<CarImage>>> GetCarImagesByCarIdAsync(Guid carId);
        Task<Result<Unit>> SetMainCarImageAsync(Guid id);
        Task<CarImageDto> ToDto(CarImage image);
        Task<List<CarImageDto>> ToDto(List<CarImage> images);
    }
}