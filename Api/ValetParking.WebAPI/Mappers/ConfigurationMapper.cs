using AutoMapper;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;

namespace ValetParking.WebApi.Mappers
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {

            CreateMap<ConfigurationEntity, ConfigurationDomain>()
                 .ForMember(destinationMember => destinationMember.ConfigurationKey, memberOption => memberOption.MapFrom(source => source.ConfigurationKey)).ReverseMap()
                 .ForMember(destinationMember => destinationMember.JsonData, memberOption => memberOption.MapFrom(source => source.JsonData)).ReverseMap()
                 .ForMember(destinationMember => destinationMember.Id, memberOption => memberOption.MapFrom(source => source.Id)).ReverseMap();

            CreateMap<ConfigurationDomain, ConfigurationDto>()
                .ForMember(destinationMember => destinationMember.ConfigurationKey, memberOption => memberOption.MapFrom(source => source.ConfigurationKey)).ReverseMap()
                .ForMember(destinationMember => destinationMember.JsonData, memberOption => memberOption.MapFrom(source => source.JsonData)).ReverseMap();          

            }
        }
}
