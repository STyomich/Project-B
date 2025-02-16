using Application.Services.CarImageService;
using Core.DTOs.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarImagesController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCarImage(CarImageDto carImageDto)
        {
            return HandleResult(await Mediator.Send(new CreateCarImage.Command { carImageDto = carImageDto }));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCarImage(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteCarImage.Command { Id = id }));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> SetMainCarImage(Guid id)
        {
            return HandleResult(await Mediator.Send(new SetMainCarImage.Command { Id = id }));
        }
        [HttpGet("{carId}")]
        public async Task<ActionResult<CarImageDto>> GetMainCarImageByCarId(Guid carId)
        {
            return HandleResult(await Mediator.Send(new GetMainCarImageByCarId.Query { CarId = carId }));
        }
        [HttpGet("{carId}/list")]
        public async Task<ActionResult<List<CarImageDto>>> GetCarImagesByCarId(Guid carId)
        {
            return HandleResult(await Mediator.Send(new GetCarImagesByCarId.Query { CarId = carId }));
        }
    }
}