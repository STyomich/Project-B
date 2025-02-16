using Application.Helpers;
using Application.Interfaces;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.RegistrationPlateService
{
    public class EditRegistrationPlate
    {
        public class Command : IRequest<Result<RegistrationPlateDto>>
        {
            public RegistrationPlateDto? RegistrationPlate { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<RegistrationPlateDto>>
        {
            private readonly IRegistrationPlateService _registrationPlateService;
            public Handler(IRegistrationPlateService registrationPlateService)
            {
                _registrationPlateService = registrationPlateService;
            }

            public async Task<Result<RegistrationPlateDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _registrationPlateService.UpdateAsync(request.RegistrationPlate!.Id, request.RegistrationPlate);
                if (!result.IsSuccess) return Result<RegistrationPlateDto>.Failure(result.Error ?? "Unknown error");
                return Result<RegistrationPlateDto>.Success(request.RegistrationPlate);
            }
        }
    }
}