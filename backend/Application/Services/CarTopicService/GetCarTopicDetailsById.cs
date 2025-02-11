using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarTopicService
{
    public class GetCarTopicDetailsById
    {
        public class Query : IRequest<Result<CarTopicDto>>
        {
            public Guid CarTopicId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<CarTopicDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CarTopicDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var carTopic = await _context.CarTopics
                    .FirstOrDefaultAsync(c => c.Id == request.CarTopicId);
                if (carTopic == null) return Result<CarTopicDto>.Failure("Car topic not found");
                var carTopicDto = _mapper.Map<CarTopicDto>(carTopic);
                return Result<CarTopicDto>.Success(carTopicDto);
            }
        }
    }
}