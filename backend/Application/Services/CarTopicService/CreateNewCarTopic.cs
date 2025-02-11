using Application.Helpers;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;

namespace Application.Services.CarTopicService
{
    public class CreateNewCarTopic
    {
        public class Command : IRequest<Result<CarTopicDto>>
        {
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
                var carTopic = _mapper.Map<CarTopic>(request.CarTopic);
                await _context.CarTopics.AddAsync(carTopic);
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    return Result<CarTopicDto>.Failure("Failed to create car topic");
                }
                return Result<CarTopicDto>.Success(_mapper.Map<CarTopicDto>(carTopic));
            }
        }
    }
}