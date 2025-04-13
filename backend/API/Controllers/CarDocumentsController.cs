using Application.Services.CarDocumentsService;
using Core.DTOs.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarDocumentsController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCarDocuments([FromForm] IFormFile file, [FromQuery] Guid carId)
        {
            return HandleResult(await Mediator.Send(new CreateCarDocuments.Command { Document = file, CarId = carId }));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCarDocuments(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteCarDocuments.Command { Id = id }));
        }
        [HttpGet("{carId}")]
        public async Task<ActionResult<CarDocumentsDto>> GetCarDocumentsByCarId(Guid carId)
        {
            return HandleResult(await Mediator.Send(new GetCarDocumentsByCarId.Query { CarId = carId }));
        }
    }
}