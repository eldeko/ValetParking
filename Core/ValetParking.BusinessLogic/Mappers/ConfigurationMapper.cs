namespace ValetParking.BusinessLogic.Mappers
{
    using AutoMapper;
    using Models.Domain;
    using ValetParking.Dto;
    using Persistence.Entities;
    public class ConfigurationMapper : Profile
    {
        //TODO: Delete all AutoMapper Stuff
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
