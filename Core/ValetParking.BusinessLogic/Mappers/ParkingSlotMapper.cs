using System;
using System.Collections.Generic;
using System.Text;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using AutoMapper;
using ValetParking.Dto;

namespace ValetParking.BusinessLogic.Mappers
{    
    public class ParkingSlotMapper : Profile
    {
        //TODO: Delete all AutoMapper Stuff
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
