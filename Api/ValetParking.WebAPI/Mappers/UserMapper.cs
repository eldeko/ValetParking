using AutoMapper;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;

namespace ValetParking.WebApi.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserEntity, UserDomain>()
                .ForMember(destinationMember => destinationMember.Name, memberOption => memberOption.MapFrom(source => source.Name)).ReverseMap()
                .ForMember(destinationMember => destinationMember.Surname, memberOption => memberOption.MapFrom(source => source.Surname)).ReverseMap()
                .ForMember(destinationMember => destinationMember.IsActive, memberOption => memberOption.MapFrom(source => source.IsActive)).ReverseMap()
                .ForAllOtherMembers(opts => opts.Ignore());
               

            CreateMap<UserDto, UserDomain>()
               .ForMember(destinationMember => destinationMember.Name, memberOption => memberOption.MapFrom(source => source.Name)).ReverseMap()
               .ForMember(destinationMember => destinationMember.IsActive, memberOption => memberOption.MapFrom(source => source.IsActive))
                .ForAllOtherMembers(opts => opts.Ignore());

            }
        }
}
