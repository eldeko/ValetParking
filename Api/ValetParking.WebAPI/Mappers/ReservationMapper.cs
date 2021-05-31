using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;

namespace ValetParking.WebApi.Mappers
    {
    public class ReservationMapper : Profile
    {
        public ReservationMapper()
        {

            CreateMap<ReservationEntity, ReservationDomain>()
                .ReverseMap();
            CreateMap<ReservationDomain, ReservationDto>()
                .ReverseMap();
            CreateMap<ReservationEntity, ReservationDto>()
                .ReverseMap();
        }
    }
}
