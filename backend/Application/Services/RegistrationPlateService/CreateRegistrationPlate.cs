using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using Core.DTOs.Entities;
using MediatR;

namespace Application.Services.RegistrationPlateService
{
    public class CreateRegistrationPlate
    {
        public class Command : IRequest<Result<RegistrationPlateDto>>
        {
            public RegistrationPlateDto? RegistrationPlateDto { get; set; }
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
                if (request.RegistrationPlateDto == null)
                {
                    return Result<RegistrationPlateDto>.Failure("Registration plate not found");
                }
                var result = await _registrationPlateService.CreateAsync(request.RegistrationPlateDto);
                if (result.IsSuccess)
                {
                    return Result<RegistrationPlateDto>.Success(request.RegistrationPlateDto);
                }
                return Result<RegistrationPlateDto>.Failure(result.Error ?? "Unknown error");
            }
        }
    }
}