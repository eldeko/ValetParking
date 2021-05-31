using AutoMapper;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;

namespace ValetParking.WebApi.Mappers
{
    public class ParkingSlotMapper : Profile
    {
        public ParkingSlotMapper()
        {
            CreateMap<ParkingSlotEntity, ParkingSlotDomain>()
                .ReverseMap();

            CreateMap<ParkingSlotDomain, ParkingSlotDto>()
                .ReverseMap();

            CreateMap<ParkingSlotEntity, ParkingSlotDto>()
                .ReverseMap();
        }
    }
}