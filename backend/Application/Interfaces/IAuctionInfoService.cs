using Application.Helpers;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Interfaces
{
    public interface IAuctionInfoService
    {
        Task<Result<Unit>> CreateAsync(AuctionInfo auction);
        Task<Result<Unit>> DeleteAsync(Guid id);
        Task<Result<AuctionInfo>> GetAuctionInfoByCarIdAsync(Guid carId);
        Task<Result<AuctionInfo>> GetAuctionInfoByIdAsync(Guid id);
        Task<Result<List<AuctionInfoListItemDto>>> GetAllAuctionsListAsync();
    }
}