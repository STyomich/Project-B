using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.OrganizationService
{
    public class DeleteOrganizationById
    {
        public class Command : IRequest<Result<Unit>>
        {
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
                _context.Organizations.Remove(organization);
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                    return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Failed to delete organization");
            }
        }
    }
}