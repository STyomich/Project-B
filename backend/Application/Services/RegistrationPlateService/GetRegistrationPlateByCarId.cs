using Application.Helpers;
using Application.Interfaces;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.RegistrationPlateService
{
    public class GetRegistrationPlateByCarId
    {
        public class Query : IRequest<Result<RegistrationPlateDto>>
        {
            public Guid CarId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<RegistrationPlateDto>>
        {
            private readonly IRegistrationPlateService _registrationPlateService;
            public Handler(IRegistrationPlateService registrationPlateService)
            {
                _registrationPlateService = registrationPlateService;
            }

            public async Task<Result<RegistrationPlateDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _registrationPlateService.GetByCarIdAsync(request.CarId);
                if (result.IsSuccess)
                {
                    if (result.Value != null)
                        return Result<RegistrationPlateDto>.Success(await _registrationPlateService.ToDto(result.Value));
                    return Result<RegistrationPlateDto>.Failure("Registration plate not found");
                }
                return Result<RegistrationPlateDto>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}