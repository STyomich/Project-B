using Application.Services.AuctionInfoService;
using Core.DTOs.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuctionInfosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAuctionInfo(AuctionInfoCreateRequest auctionInfoCreateRequest)
        {
            return HandleResult(await Mediator.Send(new CreateAuctionInfo.Command { AuctionInfo = auctionInfoCreateRequest }));
        }
        [HttpGet]
        public async Task<IActionResult> ListAuctionInfo()
        {
            return HandleResult(await Mediator.Send(new ListAuctionInfo.Query()));
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetAuctionByCarId(Guid carId)
        {
            return HandleResult(await Mediator.Send(new GetAuctionByCarId.Query { CarId = carId }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionInfo(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteAuctionInfo.Command { Id = id }));
        }
    }
}