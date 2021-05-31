using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace ValetParking.BusinessLogic.Interfaces
{
    public interface IParkingSlotService
    {
        IEnumerable<ParkingSlotEntity> GetFreeParkingSlotsByDate(DateTime fromDate, DateTime toDate);

        IEnumerable<ParkingSlotDomain> GetReservedParkingSlotsByDate(DateTime fromDate, DateTime toDate);

        IEnumerable<ParkingSlotEntity> GetAll();

        IEnumerable<ParkingSlotEntity> GetFreeParkingSlotsByHour(IEnumerable<(DateTime fromDate, DateTime toDate)> hourRanges);
    }
}