using AutoMapper;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValetParking.BusinessLogic.Business
{
    public class ParkingSlotService : IParkingSlotService
    {
        private readonly IParkingSlotRepository _parkingSlotRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        public ParkingSlotService(IParkingSlotRepository parkingSlotRepository, IReservationRepository reservationRepository, IMapper mapper)
        {
            _parkingSlotRepository = parkingSlotRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        

        public IEnumerable<ParkingSlotDomain> GetReservedParkingSlotsByDate(DateTime fromDate, DateTime toDate)
        {            
            List<ReservationEntity> reservations = _reservationRepository.GetAllWithVehicles().Where(x => x.DateFrom >= fromDate && (x.DateTo <= toDate || toDate == null)).ToList();
                      
            List <ParkingSlotEntity> reservedSlots = reservations.Select(x => x.ParkingSlot).ToList();
             var reservedParkingSlotsToDomain = _mapper.Map<List<ParkingSlotDomain>>(reservedSlots);
            return reservedParkingSlotsToDomain;
        }

        public IEnumerable<ParkingSlotEntity> GetAll()
        {
            return _parkingSlotRepository.GetAll();
        }      

        public IEnumerable<ParkingSlotEntity> GetFreeParkingSlotsByDate(DateTime fromDate, DateTime toDate)
            {
            throw new NotImplementedException();
            }

        public IEnumerable<ParkingSlotEntity> GetCompleteParkingLot(DateTime fromDate, DateTime toDate)
        {
            //TODO
            return new List<ParkingSlotEntity>();
        }

        public IEnumerable<ParkingSlotEntity> GetFreeParkingSlotsByHour(IEnumerable<(DateTime fromDate, DateTime toDate)> hourRanges)
        {
            var reservedSlots = (from res in _reservationRepository.GetAllWithVehicles()
                                from cte in hourRanges
                                where !(cte.fromDate > res.DateTo || cte.toDate < res.DateFrom) || !(res.DateFrom > cte.toDate || res.DateTo < cte.fromDate)
                                select res.ParkingSlot.Id)
                                .ToList();

            var result = _parkingSlotRepository.GetAll()
                .Where(ps => !reservedSlots.Contains(ps.Id))
                .ToList();

            return result;
        }
    }
}

