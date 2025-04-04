using Application.Helpers;
using AutoMapper;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarTopicService
{
    public class GetCarTopics
    {
        public class Query : IRequest<Result<List<CarTopicDto>>>
        {
            public string? CarName { get; set; }
            public string? CarModel { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<CarTopicDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<CarTopicDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var search = $"{request.CarName} {request.CarModel}".ToLower();

                var carTopics = await _context.CarTopics
                    .Where(c =>
                        (c.CarName + " " + c.CarModel).ToLower().Contains(search))
                    .ToListAsync();
                if (carTopics == null || !carTopics.Any()) return Result<List<CarTopicDto>>.Failure("No car topics found");
                var carTopicDtos = _mapper.Map<List<CarTopicDto>>(carTopics);
                return Result<List<CarTopicDto>>.Success(carTopicDtos);
            }
        }
    }
}