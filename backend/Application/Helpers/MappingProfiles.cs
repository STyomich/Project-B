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
            CreateMap<RegisterValues, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserListItemDto>();
            CreateMap<ApplicationUser, UserShortInfo>();

            // Car
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<Car, CarListItemDto>()
                .ForMember(dest => dest.CarMainImage,
                    opt => opt.MapFrom(src =>
                        src.CarImages != null ? src.CarImages.FirstOrDefault(img => img.isMain) : null))
                .ForMember(dest => dest.CarTopic, opt => opt.MapFrom(src => src.CarTopic));
            CreateMap<Car, CarDetailsDto>();

            // CarTopic
            CreateMap<CarTopic, CarTopicDto>();
            CreateMap<CarTopicDto, CarTopic>();

            // RegistrationPlate
            CreateMap<RegistrationPlate, RegistrationPlateDto>();
            CreateMap<RegistrationPlateDto, RegistrationPlate>();

            // CarImage
            CreateMap<CarImage, CarImageDto>();
            CreateMap<CarImageDto, CarImage>();

            // CarDocuments
            CreateMap<CarDocuments, CarDocumentsDto>();
            CreateMap<CarDocumentsDto, CarDocuments>();

            // Auctions
            CreateMap<AuctionInfo, AuctionInfoDto>()
           .ForMember(dest => dest.Owner, opt =>
               opt.MapFrom(src => src.Car != null && src.Car.User != null ? src.Car.User : null))
           .ForMember(dest => dest.Car, opt =>
               opt.MapFrom(src => src.Car));
            CreateMap<AuctionInfoDto, AuctionInfoCreateRequest>();
            CreateMap<AuctionInfoCreateRequest, AuctionInfo>();
            CreateMap<AuctionInfoDto, AuctionInfo>();
            CreateMap<AuctionInfo, AuctionInfoListItemDto>()
            .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
            .ForMember(dest => dest.MaxBid, opt => opt.MapFrom(src =>
                src.AuctionBids != null && src.AuctionBids.Any()
                    ? src.AuctionBids.OrderByDescending(b => b.BidAmount).FirstOrDefault()
                    : null
            ));
            CreateMap<AuctionInfoListItemDto, AuctionInfo>();

            // Auction Bids
            CreateMap<AuctionBid, AuctionBidDto>();
            CreateMap<AuctionBidDto, AuctionBid>();
        }
    }
}