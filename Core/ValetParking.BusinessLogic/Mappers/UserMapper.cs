using AutoMapper;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;

namespace ValetParking.BusinessLogic.Mappers
{
    public class UserMapper : Profile
    {
        //TODO: Delete all AutoMapper Stuff
        public UserMapper()
        {
            CreateMap<UserEntity, UserDomain>()
                .ForMember(destinationMember => destinationMember.Name, memberOption => memberOption.MapFrom(source => source.Name)).ReverseMap()
                .ForMember(destinationMember => destinationMember.Surname, memberOption => memberOption.MapFrom(source => source.Surname)).ReverseMap()
                .ForMember(destinationMember => destinationMember.IsActive, memberOption => memberOption.MapFrom(source => source.IsActive))
                .ReverseMap();

            CreateMap<UserDto, UserDomain>()
               .ForMember(destinationMember => destinationMember.Name, memberOption => memberOption.MapFrom(source => source.Name)).ReverseMap()
               .ForMember(destinationMember => destinationMember.IsActive, memberOption => memberOption.MapFrom(source => source.IsActive)).ReverseMap();
        }
    }
}
