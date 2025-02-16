using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.IdentityEntities;
using Core.DTOs.Entities;
using Core.DTOs.Identity;

namespace Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // User
            CreateMap<RegisterViaIdentityValues, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserListItemDto>();
            
            // Car
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<Car, CarListItemDto>()
                .ForMember(dest => dest.CarMainImage, opt => opt.MapFrom(src => src.CarImages != null ? src.CarImages.Where(img => img.isMain) : null))
                .ForMember(dest => dest.CarTopic, opt => opt.MapFrom(src => src.CarTopic));;
            CreateMap<Car, CarDetailsDto>();
            
            // CarTopic
            CreateMap<CarTopic, CarTopicDto>();
            CreateMap<CarTopicDto, CarTopic>();

            // RegistrationPlate
            CreateMap<RegistrationPlate, RegistrationPlateDto>();
            CreateMap<RegistrationPlateDto, RegistrationPlate>();
        }
    }
}