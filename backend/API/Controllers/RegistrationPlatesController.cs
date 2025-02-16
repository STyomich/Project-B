using Application.Services.RegistrationPlateService;
using Core.DTOs.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RegistrationPlatesController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateRegistrationPlate(RegistrationPlateDto registrationPlateDto)
        {
            return HandleResult(await Mediator.Send(new CreateRegistrationPlate.Command { RegistrationPlateDto = registrationPlateDto }));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> EditRegistrationPlate(Guid id, RegistrationPlateDto registrationPlateDto)
        {
            registrationPlateDto.Id = id;
            return HandleResult(await Mediator.Send(new EditRegistrationPlate.Command { RegistrationPlate = registrationPlateDto }));
        }
        [HttpGet("{carId}")]
        public async Task<ActionResult<RegistrationPlateDto>> GetRegistrationPlateByCarId(Guid carId)
        {
            return HandleResult(await Mediator.Send(new GetRegistrationPlateByCarId.Query { CarId = carId }));
        }
    }
}