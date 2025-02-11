using Application.Services.CarTopicService;
using Core.DTOs.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarTopicsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCarTopic(CarTopicDto carTopicDto)
        {
            return HandleResult(await Mediator.Send(new CreateNewCarTopic.Command { CarTopic = carTopicDto }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarTopic(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteCarTopicById.Command { CarTopicId = id }));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCarTopic(Guid id, CarTopicDto carTopicDto)
        {
            carTopicDto.Id = id;
            return HandleResult(await Mediator.Send(new EditCarTopicDetails.Command { CarTopicId = id, CarTopic = carTopicDto }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarTopicDetails(Guid id)
        {
            return HandleResult(await Mediator.Send(new GetCarTopicDetailsById.Query { CarTopicId = id }));
        }
    }
}