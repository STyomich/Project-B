using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.OrganizationService
{
    public class KickUserFromOrganization
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid UserId { get; set; }
            public Guid OrganizationId { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var organization = await _context.Organizations.FindAsync(request.OrganizationId);
                if (organization == null)
                    return Result<Unit>.Failure("Organization not found");
                if (organization.AdministratorId == request.UserId)
                    return Result<Unit>.Failure("Cannot kick the administrator");
                var user = await _context.Users.FindAsync(request.UserId);
                if (user == null)
                    return Result<Unit>.Failure("User not found");
                var organizationPin = await _context.OrganizationPins.FirstOrDefaultAsync(x => x.OrganizationId == request.OrganizationId && x.UserId == request.UserId);
                if (organizationPin == null)
                    return Result<Unit>.Failure("User not in organization");
                _context.OrganizationPins.Remove(organizationPin);
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                    return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Failed to kick user from organization");
            }
        }
    }
}