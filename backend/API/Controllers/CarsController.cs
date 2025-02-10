using Application.Services.CarService;
using Core.DTOs.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCar(CarDto carDto)
        {
            return HandleResult(await Mediator.Send(new CreateNewCar.Command { Car = carDto }));
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUsersCars(Guid userId)
        {
            return HandleResult(await Mediator.Send(new UsersCarListById.Query { UserId = userId }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteCarById.Command { Id = id }));
        }
    }
}