using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValetParking.Persistence.Repositories.Contracts
{
    public interface IParkingSlotRepository : IRepository<ParkingSlotEntity>
    {
        IEnumerable<ParkingSlotEntity> GetFullParkingLotWithReservations();
    }
}