using Application.Helpers;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CarService
{
    public class DeleteCarById
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var car = _context.Cars.FirstOrDefault(c => c.Id == request.Id);
                if (car == null)
                    return Result<Unit>.Failure("Car not found");
                _context.Cars.Remove(car);
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                    return Result<Unit>.Failure("Failed to delete car");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}