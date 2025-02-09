using AutoMapper;
using Core.Domain.IdentityEntities;
using Core.DTOs.Identity;

namespace Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterViaIdentityValues, ApplicationUser>();
        }
    }
}