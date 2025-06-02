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
        [HttpGet("is-live")]
        public async Task<IActionResult> ListLiveAuctionInfo()
        {
            return HandleResult(await Mediator.Send(new ListLiveAuctionInfo.Query()));
        }
        [HttpGet("upcoming")]
        public async Task<IActionResult> ListUpcomingAuctionInfo()
        {
            return HandleResult(await Mediator.Send(new ListUpcomingAuctionInfo.Query()));
        }
        [HttpGet("deprecated")]
        public async Task<IActionResult> ListDeprecatedAuctionInfo()
        {
            return HandleResult(await Mediator.Send(new ListDeprecatedAuctionInfo.Query()));
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
        [HttpGet("whole-info/{id}")]
        public async Task<IActionResult> GetAuctionInfoById(Guid id)
        {
            return HandleResult(await Mediator.Send(new GetAuctionInfoById.Query { Id = id }));
        }
        [HttpGet("users-auctions")]
        public async Task<IActionResult> GetAuctionInfoListWithBidViaClaims()
        {
            return HandleResult(await Mediator.Send(new GetAuctionInfoListWithBidViaClaims.Query { User = User }));
        }
    }
}