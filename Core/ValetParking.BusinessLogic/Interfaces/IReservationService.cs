using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using ValetParking.Persistence.Entities;

namespace ValetParking.BusinessLogic.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<DateTime> GetUnavailableDaysForParking();

        //IList<ReservationEntity> GetReservationsByUser(string email);
        //IList<ReservationEntity> GetReservationsByDateRange(DateTime fromDate, DateTime toDate);
    }
}

