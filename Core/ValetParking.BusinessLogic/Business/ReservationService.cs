using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Persistence.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValetParking.BusinessLogic.Business
{
    //Written by Edward.
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IParkingSlotRepository _parkingSlotRepository;
        private const int MAX_PARKING_TIME_MINUTES_PER_DAY = 24 * 60; // Assume that parking works 24 hours / day
        private const int MIN_PARKING_TIME_MINUTES_PER_DAY = 15; // Assume that minimum parking time is 15 minutes

        public ReservationService(IReservationRepository reservationRepository, IParkingSlotRepository parkingSlotRepository)
        {
            _reservationRepository = reservationRepository;
            _parkingSlotRepository = parkingSlotRepository;
        }

        /// <summary>
        /// Get the list of days that are available for parking, assumming that:
        /// An available day is the one which has at least 15 minutes free.
        /// </summary>
        /// <returns>Available parking days list.</returns>
        public IEnumerable<DateTime> GetUnavailableDaysForParking()
        {
            // Max parking time - min parking time determines the min parking time for full occupation each day
            int fullParkingOccupationTimeMinutes = MAX_PARKING_TIME_MINUTES_PER_DAY - MIN_PARKING_TIME_MINUTES_PER_DAY;

            var fullParkingSlotsByDate =
                from reservation in _reservationRepository.GetAll()
                group reservation by new { reservation.DateFrom.Date, reservation.ParkingSlot } into reservationGroup
                where reservationGroup.Sum(g => ((g.DateTo.Value != null ? g.DateTo.Value : g.DateFrom) - g.DateFrom).TotalMinutes) >= fullParkingOccupationTimeMinutes
                select new
                {
                    Date = reservationGroup.Key.Date,
                    TotalOccupationMinutes = reservationGroup.Sum(g => ((g.DateTo.Value != null ? g.DateTo.Value : g.DateFrom) - g.DateFrom).TotalMinutes)
                };

            int parkingSlotsCount = _parkingSlotRepository.GetAll().Where(ps => ps.IsActive).Count();

            var unavailableDays =
                from fullParkingSlot in fullParkingSlotsByDate
                group fullParkingSlot by fullParkingSlot.Date into fullParkingSlotGroup
                where fullParkingSlotGroup.Count() >= parkingSlotsCount
                select fullParkingSlotGroup.Key.Date;

            return unavailableDays.ToList();
        }

        //public IList<ReservationEntity> GetReservationsByUser(string email)
        //{
        //    return _reservationRepository.GetReservationsByUser(email);
        //}

        //public IList<ReservationEntity> GetReservationsByDateRange(DateTime fromDate, DateTime toDate)
        //{
        //    return _reservationRepository.GetReservationsByDateRange(fromDate, toDate);
        //}
    }
}