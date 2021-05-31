using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace ValetParking.Persistence.Repositories.Contracts
    {
    public interface IReservationRepository : IRepository<ReservationEntity>
    {
        IEnumerable<ReservationEntity> GetAllWithVehicles();
        //IList<ReservationEntity> GetReservationsByDateRange(DateTime fromDate, DateTime toDate);
    }
}