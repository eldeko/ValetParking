using Microsoft.EntityFrameworkCore;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System.Collections.Generic;

namespace ValetParking.Persistence.Repositories
{
    public class ParkingSlotRepository : Repository<ParkingSlotEntity>, IParkingSlotRepository
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        public ParkingSlotRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<ParkingSlotEntity> GetFullParkingLotWithReservations()
        {
            return _unitOfWorkFactory.Current.Context.ParkingSlots
                      .Include(parkingslots => parkingslots.Reservations);
                  
        }

        public IEnumerable<ParkingSlotEntity> GetFreeParkingLotWithParkingSlots()
        {
            return _unitOfWorkFactory.Current.Context.ParkingSlots
                      .Include(parkingslots => parkingslots.Reservations);

        }
    }
}

