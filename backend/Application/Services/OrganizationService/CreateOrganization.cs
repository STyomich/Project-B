using Application.Helpers;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.OrganizationService
{
    public class CreateOrganization
    {
        public class Command : IRequest<Result<OrganizationDto>>
        {
            public OrganizationDto? OrganizationDto { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<OrganizationDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<OrganizationDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.OrganizationDto == null)
                {
                    return Result<OrganizationDto>.Failure("Organization not found");
                }
                var organization = _mapper.Map<Organization>(request.OrganizationDto);
                _context.Organizations.Add(organization);
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    return Result<OrganizationDto>.Success(_mapper.Map<OrganizationDto>(organization));
                }
                return Result<OrganizationDto>.Failure("Failed to create organization");
            }
        }
    }
}