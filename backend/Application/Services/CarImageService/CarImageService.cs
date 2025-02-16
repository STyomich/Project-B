using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarImageService
{
    public class CarImageService : ICarImageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CarImageService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> CreateAsync(CarImageDto dto)
        {
            var image = _mapper.Map<CarImage>(dto);
            if (_context.CarImages.Any(x => x.CarId == image.CarId && x.isMain)) return Result<Unit>.Failure("Car already has a main image");
            _context.CarImages.Add(image);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to create car image");
            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<Unit>> DeleteAsync(Guid id)
        {
            if (_context.CarImages.Any(x => x.Id == id && x.isMain)) return Result<Unit>.Failure("Main car image cannot be deleted");
            var image = await _context.CarImages.FindAsync(id);
            if (image == null) return Result<Unit>.Failure("Car image not found");
            _context.CarImages.Remove(image);
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to delete car image");
            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<CarImage>> GetMainCarImageByCarIdAsync(Guid carId)
        {
            var image = await _context.CarImages.FirstOrDefaultAsync(x => x.CarId == carId && x.isMain == true);
            if (image == null) return Result<CarImage>.Failure("Car image not found");
            return Result<CarImage>.Success(image);
        }
        public async Task<Result<List<CarImage>>> GetCarImagesByCarIdAsync(Guid carId)
        {
            var images = await _context.CarImages.Where(x => x.CarId == carId).ToListAsync();
            if (images == null) return Result<List<CarImage>>.Failure("Car image not found");
            return Result<List<CarImage>>.Success(images);
        }
        public async Task<Result<Unit>> SetMainCarImageAsync(Guid id)
        {
            var image = await _context.CarImages.FindAsync(id);
            if (image == null) return Result<Unit>.Failure("Car image not found");
            var mainImage = await _context.CarImages.FirstOrDefaultAsync(x => x.CarId == image.CarId && x.isMain);
            if (mainImage != null) mainImage.isMain = false;
            image.isMain = true;
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) return Result<Unit>.Failure("Failed to set main car image");
            return Result<Unit>.Success(Unit.Value);
        }
        public Task<CarImageDto> ToDto(CarImage image)
        {
            return Task.FromResult(_mapper.Map<CarImageDto>(image));
        }
        public Task<List<CarImageDto>>ToDto(List<CarImage> images)
        {
            return Task.FromResult(_mapper.Map<List<CarImageDto>>(images));
        }
    }
}