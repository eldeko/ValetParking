using Microsoft.EntityFrameworkCore;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System.Collections.Generic;

namespace ValetParking.Persistence.Repositories
{
    public class ReservationRepository : Repository<ReservationEntity>, IReservationRepository
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public ReservationRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<ReservationEntity> GetAllWithVehicles()
        {
            return _unitOfWorkFactory.Current.Context.Reservations.Include("ParkingSlot");
        }
    }
}