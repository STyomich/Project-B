using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.OrganizationService
{
    public class OrganizationDetailsById
    {
        public class Query : IRequest<Result<OrganizationDto>>
        {
            public Guid OrganizationId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<OrganizationDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<OrganizationDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var organization = await _context.Organizations.FindAsync(request.OrganizationId);
                if (organization == null)
                    return Result<OrganizationDto>.Failure("Organization not found");
                return Result<OrganizationDto>.Success(_mapper.Map<OrganizationDto>(organization));
            }
        }
    }
}