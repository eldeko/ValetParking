using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.Repositories
{
    public class VehicleRepository : Repository<VehicleEntity>, IVehicleRepository
    {
        public VehicleRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }
    }
}
