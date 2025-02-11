using Application.Helpers;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarTopicService
{
    public class EditCarTopicDetails
    {
        public class Command : IRequest<Result<CarTopicDto>>
        {
            public Guid CarTopicId { get; set; }
            public CarTopicDto? CarTopic { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CarTopicDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<CarTopicDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.CarTopic == null)
                {
                    return Result<CarTopicDto>.Failure("Car topic is required");
                }
                var carTopic = await _context.CarTopics.FirstOrDefaultAsync(x => x.Id == request.CarTopicId);
                if (carTopic == null)
                {
                    return Result<CarTopicDto>.Failure("Car topic not found");
                }
                carTopic = _mapper.Map<CarTopic>(request.CarTopic);
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    return Result<CarTopicDto>.Failure("Failed to update car topic");
                }
                return Result<CarTopicDto>.Success(_mapper.Map<CarTopicDto>(carTopic));
            }
        }
    }
}