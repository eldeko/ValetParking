using AutoMapper;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;

namespace ValetParking.BusinessLogic.Mappers
{
    public class VehicleMapper : Profile
    {
        //TODO: Delete all AutoMapper Stuff
        public VehicleMapper()
        {
            CreateMap<VehicleEntity, VehicleDomain>()

                    .ForMember(destinationMember => destinationMember.Brand, memberOption => memberOption.MapFrom(source => source.Brand)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.Color, memberOption => memberOption.MapFrom(source => source.Color)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.IsActive, memberOption => memberOption.MapFrom(source => source.IsActive)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.LicensePlate, memberOption => memberOption.MapFrom(source => source.LicensePlate)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.VehicleType, memberOption => memberOption.MapFrom(source => source.VehicleType)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.Model, memberOption => memberOption.MapFrom(source => source.Model)).ReverseMap()
                    .ForAllOtherMembers(opts => opts.Ignore());



            CreateMap<VehicleDomain, VehicleDto>()
                    .ForMember(destinationMember => destinationMember.Brand, memberOption => memberOption.MapFrom(source => source.Brand)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.Color, memberOption => memberOption.MapFrom(source => source.Color)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.IsActive, memberOption => memberOption.MapFrom(source => source.IsActive)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.LicensePlate, memberOption => memberOption.MapFrom(source => source.LicensePlate)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.VehicleType, memberOption => memberOption.MapFrom(source => source.VehicleType)).ReverseMap()
                    .ForMember(destinationMember => destinationMember.Model, memberOption => memberOption.MapFrom(source => source.Model)).ReverseMap()
                    .ForAllOtherMembers(opts => opts.Ignore());
        }
    }
}
