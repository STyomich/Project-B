using Core.DTOs.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuctionInfosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAuctionInfo(AuctionInfoDto auctionInfoDto)
        {
            return HandleResult(await Mediator.Send(new Application.Services.AuctionInfoService.CreateAuctionInfo.Command { AuctionInfo = auctionInfoDto }));
        }
        [HttpGet]
        public async Task<IActionResult> ListAuctionInfo()
        {
            return HandleResult(await Mediator.Send(new Application.Services.AuctionInfoService.ListAuctionInfo.Query()));
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetAuctionByCarId(Guid carId)
        {
            return HandleResult(await Mediator.Send(new Application.Services.AuctionInfoService.GetAuctionByCarId.Query { CarId = carId }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionInfo(Guid id)
        {
            return HandleResult(await Mediator.Send(new Application.Services.AuctionInfoService.DeleteAuctionInfo.Command { Id = id }));
        }
    }
}